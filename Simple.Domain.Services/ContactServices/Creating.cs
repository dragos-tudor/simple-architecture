
using System.Threading;

namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static async Task<Result<ContactCreatedEvent?, string[]?>> CreateContactService (
    Contact contact,
    FindModels<PhoneNumber, long> findPhoneNumbers,
    CancellationToken cancellationToken = default)
  {
    SetContactId(contact, GenerateContactId());

    var duplicateNumbers = await findPhoneNumbers(contact.PhoneNumbers ?? [], cancellationToken);
    if (ExistsPhoneNumbers(duplicateNumbers)) return AsArray(GetDuplicatePhoneNumberErrors(duplicateNumbers));

    var contactErrors = ValidateContact(contact);
    if (ExistsValidationErrors(contactErrors)) return AsArray(contactErrors);

    return CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);
  }
}