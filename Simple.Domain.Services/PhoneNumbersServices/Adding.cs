
using System.Threading;

namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static async Task<Result<Contact?, string[]?>> AddPhoneNumberService (
    Contact contact,
    PhoneNumber phoneNumber,
    FindModel<PhoneNumber, long?> findPhoneNumber,
    CancellationToken cancellationToken = default)
  {
    var phoneNumberErrors = ValidatePhoneNumber(phoneNumber);
    if (ExistsValidationErrors(phoneNumberErrors)) return AsArray(phoneNumberErrors)!;

    var duplicateNumber = await findPhoneNumber(phoneNumber, cancellationToken);
    if (ExistsPhoneNumber(duplicateNumber)) return AsArray([GetDuplicatePhoneNumberError(duplicateNumber.Value)]);

    SetContactPhoneNumber(contact, phoneNumber);

    var contactErrors = ValidateContact(contact);
    if (ExistsValidationErrors(contactErrors)) return AsArray(contactErrors);

    return contact;
  }
}