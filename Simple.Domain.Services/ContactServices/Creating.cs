
using System.Threading;

namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static async Task<Result<ContactCreatedEvent?, string[]?>> CreateContactService (
    Contact contact,
    IEnumerable<PhoneNumber> phoneNumbers,
    FindModels<PhoneNumber, long> findPhoneNumbers,
    CancellationToken cancellationToken = default)
  {
    var duplicateNumbers = await findPhoneNumbers(phoneNumbers ?? [], cancellationToken);
    if (ExistsPhoneNumbers(duplicateNumbers)) return AsArray(GetDuplicatePhoneNumberErrors(duplicateNumbers));

    SetContactId(contact, GenerateContactId());
    SetContactPhoneNumbers(contact, phoneNumbers!);

    var contactErrors = ValidateContact(contact);
    if (ExistsValidationErrors(contactErrors)) return AsArray(contactErrors);

    return CreateContactCreatedEvent(contact.ContactId, contact.ContactEmail);
  }
}