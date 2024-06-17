
using Microsoft.Extensions.Logging;

namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static readonly PhoneNumberValidator PhoneNumberValidator =  new ();

  public static async Task<Result<PhoneNumber?, Failure[]?>> AddPhoneNumberService (
    Guid contactId,
    PhoneNumber phoneNumber,
    FindModel<PhoneNumber, PhoneNumber?> findDuplicatePhoneNumber,
    FindModel<Guid, Contact?> findContact,
    SaveModels<Contact, PhoneNumber> insertPhoneNumber,
    ILogger logger,
    string? correlationId = default,
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
    await insertPhoneNumber(contact, phoneNumber, cancellationToken);

    LogPhoneNumberAdded(logger, phoneNumber.Number, contact.ContactId, correlationId);
    return phoneNumber;
  }
}