
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
    string? correlationId = default,
    CancellationToken cancellationToken = default)
  {
    var contact = await findContact(contactId, cancellationToken);
    if (!ExistsContact(contact)) return ToArray([GetMissingContactFailure(contactId)]);

    var phoneNumbers = contact!.PhoneNumbers ?? [];
    var contactPhoneNumber = FindPhoneNumber(phoneNumbers.AsQueryable(), phoneNumber).FirstOrDefault();
    if(!ExistsPhoneNumber(contactPhoneNumber)) return phoneNumber;

    await deletePhoneNumber(contact, contactPhoneNumber!, cancellationToken);

    LogPhoneNumberRemoved(logger, contactPhoneNumber!.Number, contact.ContactId, correlationId);
    return phoneNumber;
  }
}