
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static readonly PhoneNumberValidator PhoneNumberValidator =  new ();

  public static async Task<Result<Contact?, Failure[]?>> AddPhoneNumberService (
    Guid contactId,
    PhoneNumber phoneNumber,
    FindModel<PhoneNumber, PhoneNumber?> findDuplicatePhoneNumber,
    FindModel<Guid, Contact?> findContact,
    SaveModels<Contact, PhoneNumber> savePhoneNumber,
    string? traceId = default,
    CancellationToken cancellationToken = default)
  {
    var dataFailures = ValidateData(phoneNumber, PhoneNumberValidator);
    if(ExistsFailures(dataFailures)) return ToArray(dataFailures);

    var domainFailures = ValidatePhoneNumber(phoneNumber);
    if(ExistsFailures(domainFailures)) return ToArray(GetFailures(domainFailures));

    var contact = await findContact(contactId, cancellationToken);
    if (contact is null) return ToArray([GetMissingContactFailure(contactId)]);

    var duplicateNumber = await findDuplicatePhoneNumber(phoneNumber, cancellationToken);
    if (ExistPhoneNumber(duplicateNumber)) return ToArray([GetDuplicatePhoneNumberFailure(duplicateNumber!)]);

    SetContactPhoneNumber(contact, phoneNumber);
    await savePhoneNumber(contact, phoneNumber, cancellationToken);

    LogPhoneNumberAdded(Logger, phoneNumber.Number, contact.ContactId, traceId);
    return contact;
  }
}