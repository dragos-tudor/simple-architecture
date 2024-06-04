
namespace Simple.Domain.Api;

partial class ApiFuncs
{
  public static async Task<Result<Contact?, string[]?>> AddPhoneNumberApi (
    Guid contactId,
    PhoneNumber phoneNumber,
    FindModel<PhoneNumber, PhoneNumber?> findPhoneNumber,
    FindModel<Guid, Contact?> findContact,
    SaveModels<Contact, PhoneNumber> saveModels,
    string? traceId = default,
    CancellationToken cancellationToken = default)
  {
    var contact = await findContact(contactId, cancellationToken);
    if (contact is null) return AsArray([GetMissingContactError(contactId)]);

    var contactErrors = ValidateObject(contact, ContactValidator);
    if(ExistsValidationErrors(contactErrors)) return AsArray(contactErrors);

    var phoneNumberErrors = ValidateObject(phoneNumber, PhoneNumberValidator);
    if(ExistsValidationErrors(contactErrors)) return AsArray(phoneNumberErrors);

    var result = await AddPhoneNumberService(contact, phoneNumber, findPhoneNumber, cancellationToken);
    if(IsFailureResult(result)) return AsArray(FromFailure(result)!);

    await saveModels(contact, phoneNumber, cancellationToken);

    LogPhoneNumberAdded(Logger, phoneNumber.Number, contact.ContactId, traceId);
    return contact;
  }
}