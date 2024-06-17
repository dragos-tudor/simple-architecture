
using Microsoft.Extensions.Logging;

namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static readonly ContactValidator ContactValidator =  new ();

  public static async Task<Result<Contact?, Failure[]?>> CreateContactService (
    Contact contact,
    FindModels<PhoneNumber, PhoneNumber> findPhoneNumbers,
    FindModel<string, Contact?> findContactByName,
    FindModel<string, Contact?> findContactByEmail,
    SaveModels<Contact, Message<ContactCreatedEvent>> insertContactAndMessage,
    ProduceMessage<Message<ContactCreatedEvent>> produceMessage,
    ILogger logger,
    string? correlationId = default,
    CancellationToken cancellationToken = default)
  {
    var contactFailures = ValidateData(contact, ContactValidator);
    if(ExistsFailures(contactFailures)) return ToArray(contactFailures);

    var phoneNumberFailures = ValidateData(contact.PhoneNumbers ?? [], PhoneNumberValidator);
    if(ExistsFailures(phoneNumberFailures)) return ToArray(phoneNumberFailures);

    var contactDomainFailures = ValidateContact(contact);
    if (ExistsFailures(contactDomainFailures)) return ToArray(GetFailures(contactDomainFailures));

    var duplicateContactName = await findContactByName(contact.ContactName, cancellationToken);
    if (ExistContact(duplicateContactName)) return ToArray([GetDuplicateContactNameFailure(contact.ContactName)]);

    var duplicateContactEmail = await findContactByEmail(contact.ContactEmail, cancellationToken);
    if (ExistContact(duplicateContactEmail)) return ToArray([GetDuplicateContactEmailFailure(contact.ContactEmail)]);

    var duplicateNumbers = await findPhoneNumbers(contact.PhoneNumbers ?? [], cancellationToken);
    if (ExistsPhoneNumbers(duplicateNumbers)) return ToArray(GetDuplicatePhoneNumberFailures(duplicateNumbers));

    var @event = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);
    var message = CreateMessage(@event, correlationId: correlationId);

    await insertContactAndMessage(contact, message, cancellationToken);
    LogContactCreated(logger, contact.ContactId, correlationId);

    produceMessage(message);
    return contact;
  }
}