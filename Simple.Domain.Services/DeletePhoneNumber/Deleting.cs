
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
    if (!ExistContact(contact)) return ToArray([GetMissingContactFailure(contactId)]);

    var phoneNumbers = contact!.PhoneNumbers ?? [];
    var contactPhoneNumber = FindPhoneNumber(phoneNumbers.AsQueryable(), phoneNumber).FirstOrDefault();
    if(!ExistPhoneNumber(contactPhoneNumber)) return contactPhoneNumber;

    await deletePhoneNumber(contact, contactPhoneNumber!, cancellationToken);

    LogPhoneNumberRemoved(logger, contactPhoneNumber!.Number, contact.ContactId, traceId);
    return phoneNumber;
  }
}