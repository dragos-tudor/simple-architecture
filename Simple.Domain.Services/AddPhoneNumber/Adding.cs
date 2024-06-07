
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static readonly PhoneNumberValidator PhoneNumberValidator =  new ();

  public static async Task<Result<Contact?, Exception[]?>> AddPhoneNumberService (
    Guid contactId,
    PhoneNumber phoneNumber,
    FindModel<PhoneNumber, PhoneNumber?> findDuplicatePhoneNumber,
    FindModel<Guid, Contact?> findContact,
    SaveModels<Contact, PhoneNumber> savePhoneNumber,
    string? traceId = default,
    CancellationToken cancellationToken = default)
  {
    var dataErrors = ValidateData(phoneNumber, PhoneNumberValidator);
    if(ExistsErrors(dataErrors)) return ToArray(dataErrors);

    var domainErrors = ValidatePhoneNumber(phoneNumber);
    if(ExistsErrors(domainErrors)) return ToArray(GetErrors(domainErrors));

    var contact = await findContact(contactId, cancellationToken);
    if (contact is null) return ToArray([GetMissingContactError(contactId)]);

    var duplicateNumber = await findDuplicatePhoneNumber(phoneNumber, cancellationToken);
    if (ExistPhoneNumber(duplicateNumber)) return ToArray([GetDuplicatePhoneNumberError(duplicateNumber!)]);

    SetContactPhoneNumber(contact, phoneNumber);
    await savePhoneNumber(contact, phoneNumber, cancellationToken);

    LogPhoneNumberAdded(Logger, phoneNumber.Number, contact.ContactId, traceId);
    return contact;
  }
}