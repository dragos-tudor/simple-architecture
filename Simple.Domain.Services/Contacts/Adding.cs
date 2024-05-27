
using System.Threading;

namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static async Task<Result<Contact?, string[]?>> AddContactPhoneNumberService (
    Contact contact,
    PhoneNumber phoneNumber,
    FindModel<PhoneNumber, long?> findPhoneNumber,
    SaveModels<Contact, PhoneNumber> saveModels,
    CancellationToken cancellationToken = default)
  {
    var phoneNumberErrors = ValidatePhoneNumber(phoneNumber);
    if (ExistsValidationErrors(phoneNumberErrors)) return AsArray(phoneNumberErrors)!;

    var duplicateNumber = await findPhoneNumber(phoneNumber, cancellationToken);
    if (ExistsPhoneNumber(duplicateNumber)) return AsArray([GetDuplicatePhoneNumberError(duplicateNumber.Value)]);

    var contactErrors = ValidateContact(contact);
    if (ExistsValidationErrors(contactErrors)) return AsArray(contactErrors);

    await saveModels(contact, phoneNumber, cancellationToken);
    return contact;
  }
}