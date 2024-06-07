
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static readonly ContactValidator ContactValidator =  new ();

  public static async Task<Result<Contact?, Exception[]?>> CreateContactService (
    Contact contact,
    FindModels<PhoneNumber, PhoneNumber> findPhoneNumbers,
    FindModel<string, Contact?> findContactByName,
    FindModel<string, Contact?> findContactByEmail,
    SaveModels<Contact, Message<ContactCreatedEvent>> saveContactAndMessage,
    ProduceMessage<Message<ContactCreatedEvent>> produceMessage,
    string? traceId = default,
    CancellationToken cancellationToken = default)
  {
    var contactErrors = ValidateData(contact, ContactValidator);
    if(ExistsErrors(contactErrors)) return ToArray(contactErrors);

    var phoneNumberErrors = ValidateData(contact.PhoneNumbers ?? [], PhoneNumberValidator);
    if(ExistsErrors(phoneNumberErrors)) return ToArray(phoneNumberErrors);

    var contactDomainErrors = ValidateContact(contact);
    if (ExistsErrors(contactDomainErrors)) return ToArray(GetErrors(contactDomainErrors));

    var duplicateNumbers = await findPhoneNumbers(contact.PhoneNumbers ?? [], cancellationToken);
    if (ExistsPhoneNumbers(duplicateNumbers)) return ToArray(GetDuplicatePhoneNumberErrors(duplicateNumbers));

    var duplicateContactName = await findContactByName(contact.ContactName, cancellationToken);
    if (ExistContact(duplicateContactName)) return ToArray([GetDuplicateContactNameError(contact.ContactName)]);

    var duplicateContactEmail = await findContactByEmail(contact.ContactEmail, cancellationToken);
    if (ExistContact(duplicateContactEmail)) return ToArray([GetDuplicateContactEmailError(contact.ContactEmail)]);

    var @event = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);
    var message = CreateMessage(@event, traceId: traceId);

    await saveContactAndMessage(contact, message, cancellationToken);
    LogContactCreated(Logger, contact.ContactId, traceId);

    produceMessage(message);
    return contact;
  }
}