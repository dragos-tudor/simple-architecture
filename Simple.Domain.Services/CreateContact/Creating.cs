
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static async Task<Result<ContactCreatedEvent?, string?>> CreateContactAsync(
    Contact contact,
    FindModel<string, Contact?> findContactByName,
    FindModel<string, Contact?> findContactByEmail,
    StoreModels<Contact, Message<ContactCreatedEvent>> inertContactAndMessage,
    EnqueueMessage<Message<ContactCreatedEvent>> enqueueMessage,
    string correlationId,
    CancellationToken cancellationToken = default)
  {
    var valErrors = ValidateContact(contact);
    if (ExistErrors(valErrors)) return JoinErrors(valErrors);

    var contactWithDuplicatedName = await findContactByName(contact.ContactName, cancellationToken);
    if (ExistsContact(contactWithDuplicatedName)) return DuplicateContactNameError;

    var contactWithDu0licatedEmail = await findContactByEmail(contact.ContactEmail, cancellationToken);
    if (ExistsContact(contactWithDu0licatedEmail)) return DuplicateContactEmailError;

    var @event = CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);
    var message = CreateMessage(@event, correlationId: correlationId);

    await inertContactAndMessage(contact, message, cancellationToken);
    enqueueMessage(message);

    return @event;
  }
}