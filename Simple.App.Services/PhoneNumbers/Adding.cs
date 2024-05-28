
namespace Simple.App.Services;

partial class ServicesFuncs
{
  public static async Task<Result<Contact?, string[]?>> AddPhoneNumberService (
    Guid contactId,
    PhoneNumber phoneNumber,
    FindModel<PhoneNumber, long?> findPhoneNumber,
    FindModel<Guid, Contact?> findContact,
    SaveModels<Contact, PhoneNumber> saveModels,
    string? traceId = default,
    CancellationToken cancellationToken = default)
  {
    var contact = await findContact(contactId, cancellationToken);
    if (contact is null) return AsArray([GetMissingContactError(contactId)]);

    var contactErrors = ValidateModel(contact, ContactValidator);
    if(ExistsValidationErrors(contactErrors)) return AsArray(contactErrors);

    var phoneNumberErrors = ValidateModel(phoneNumber, PhoneNumberValidator);
    if(ExistsValidationErrors(contactErrors)) return AsArray(phoneNumberErrors);

    var result = await DomainFuncs.AddPhoneNumberService(contact, phoneNumber, findPhoneNumber, cancellationToken);
    if(IsFailureResult(result)) return AsArray(FromFailure(result)!);

    await saveModels(contact, phoneNumber, cancellationToken);

    LogPhoneNumberAdded(Logger, phoneNumber.Number, contact.ContactId, traceId);
    return contact;
  }
}