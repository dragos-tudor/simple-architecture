
using Microsoft.Extensions.Logging;

namespace Simple.Domain.Services;

partial class ServicesFuncs
{

  public static async Task<Result<PhoneNumber?, Failure[]?>> DeletePhoneNumberService (
    Guid contactId,
    PhoneNumber phoneNumber,
    FindModel<Guid, Contact?> findContact,
    SaveModels<Contact, PhoneNumber> deletePhoneNumber,
    ILogger logger,
    string? traceId = default,
    CancellationToken cancellationToken = default)
  {
    var contact = await findContact(contactId, cancellationToken);
    if (contact is null) return ToArray([GetMissingContactFailure(contactId)]);

    await deletePhoneNumber(contact, phoneNumber, cancellationToken);

    LogPhoneNumberRemoved(logger, phoneNumber.Number, contact.ContactId, traceId);
    return phoneNumber;
  }
}