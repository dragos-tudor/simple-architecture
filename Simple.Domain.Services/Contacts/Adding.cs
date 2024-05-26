
using System;

namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static async Task<Result<Contact?, string[]?>> AddContactPhoneNumberService (
    Contact contact,
    PhoneNumber phoneNumber,
    FindModel<PhoneNumber, long?> FindPhoneNumber,
    SaveModels<Contact, PhoneNumber> SaveModels)
  {
    var phoneNumberErrors = ValidatePhoneNumber(phoneNumber);
    if (ExistValidationErrors(phoneNumberErrors)) return AsArray(phoneNumberErrors)!;

    var duplicateNumber = await FindPhoneNumber(phoneNumber);
    if (ExistsPhoneNumber(duplicateNumber)) return AsArray([GetDuplicatePhoneNumberError(duplicateNumber.Value)]);

    var contactErrors = ValidateContact(contact);
    if (ExistValidationErrors(contactErrors)) return AsArray(contactErrors);

    await SaveModels(contact, phoneNumber);
    return contact;
  }
}