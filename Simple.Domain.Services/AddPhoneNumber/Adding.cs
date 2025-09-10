
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static async Task<Result<PhoneNumber?, string?>> AddPhoneNumberService(
    Guid contactId,
    PhoneNumber phoneNumber,
    FindModel<PhoneNumber, PhoneNumber?> findPhoneNumber,
    FindModel<Guid, Contact?> findContact,
    StoreModels<Contact, PhoneNumber> insertPhoneNumber,
    CancellationToken cancellationToken = default)
  {
    var valErrors = ValidatePhoneNumber(phoneNumber);
    if (ExistErrors(valErrors)) return JoinErrors(valErrors);

    var contact = await findContact(contactId, cancellationToken);
    if (contact is null) return MissingContactError;

    var existingPhoneNumber = await findPhoneNumber(phoneNumber, cancellationToken);
    if (ExistsPhoneNumber(existingPhoneNumber)) return DuplicatePhoneNumberError;

    SetContactPhoneNumber(contact, phoneNumber);
    await insertPhoneNumber(contact, phoneNumber, cancellationToken);

    return phoneNumber;
  }
}