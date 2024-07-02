
using Microsoft.Extensions.Logging;

namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  static readonly ContactValidator ContactValidator =  new ();

  public static async Task<Result<Contact?, Failure[]?>> CreateContactService (
    Contact contact,
    FindModels<PhoneNumber, PhoneNumber> findPhoneNumbers,
    FindModel<string, Contact?> findContactByName,
    FindModel<string, Contact?> findContactByEmail,
    SaveModels<Contact, Message<ContactCreatedEvent>> insertContactAndMessage,
    EnqueueMessage<Message<ContactCreatedEvent>> enqueueMessage,
    ILogger logger,
    string? correlationId = default,
    CancellationToken cancellationToken = default)
  {
    var contactFailures = ValidateData(contact, ContactValidator);
    if(ExistFailures(contactFailures)) return ToArray(contactFailures);

    var phoneNumberFailures = ValidateData(contact.PhoneNumbers ?? [], PhoneNumberValidator);
    if(ExistFailures(phoneNumberFailures)) return ToArray(phoneNumberFailures);

    var contactDomainFailures = ValidateContact(contact);
    if (ExistFailures(contactDomainFailures)) return ToArray(GetFailures(contactDomainFailures));

    var duplicateContactName = await findContactByName(contact.ContactName, cancellationToken);
    if (ExistsContact(duplicateContactName)) return ToArray([GetDuplicateContactNameFailure(contact.ContactName)]);

    var duplicateContactEmail = await findContactByEmail(contact.ContactEmail, cancellationToken);
    if (ExistsContact(duplicateContactEmail)) return ToArray([GetDuplicateContactEmailFailure(contact.ContactEmail)]);

    var duplicateNumbers = await findPhoneNumbers(contact.PhoneNumbers ?? [], cancellationToken);
    if (ExistPhoneNumbers(duplicateNumbers)) return ToArray(GetDuplicatePhoneNumberFailures(duplicateNumbers));

    var @event = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);
    var message = CreateMessage(@event, correlationId: correlationId);

    await insertContactAndMessage(contact, message, cancellationToken);
    LogContactCreated(logger, contact.ContactId, correlationId);

    enqueueMessage(message);
    return contact;
  }
}