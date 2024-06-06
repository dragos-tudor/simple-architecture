
using System.Threading;

namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static async Task<Result<ContactCreatedEvent?, string[]?>> CreateContactService (
    Contact contact,
    FindModels<PhoneNumber, PhoneNumber> findPhoneNumbers,
    CancellationToken cancellationToken = default)
  {
    //TODO: validate name and email uniqueness
    var duplicateNumbers = await findPhoneNumbers(contact.PhoneNumbers ?? [], cancellationToken);
    if (ExistsPhoneNumbers(duplicateNumbers)) return AsArray(GetDuplicatePhoneNumberErrors(duplicateNumbers));

    SetContactId(contact, GenerateContactId());

    var contactErrors = ValidateContact(contact);
    if (ExistsValidationErrors(contactErrors)) return AsArray(contactErrors);

    return CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);
  }
}