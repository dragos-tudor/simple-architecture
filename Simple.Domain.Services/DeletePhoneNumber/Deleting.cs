
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static async Task<Result<PhoneNumber?, string?>> DeletePhoneNumberService(
    Guid contactId,
    PhoneNumber phoneNumber,
    FindModel<Guid, Contact?> findContact,
    StoreModels<Contact, PhoneNumber> deletePhoneNumber,
    CancellationToken cancellationToken = default)
  {
    var contact = await findContact(contactId, cancellationToken);
    if (!ExistsContact(contact)) return MissingContactError;
    if (!ExistsPhoneNumber(contact!.PhoneNumbers, phoneNumber)) return MissingPhoneNumberError;

    await deletePhoneNumber(contact, phoneNumber, cancellationToken);
    return phoneNumber;
  }
}