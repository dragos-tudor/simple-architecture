
using static System.Threading.Tasks.Task;
using NSubstitute;

namespace Simple.Domain.Services;

partial class ServicesTests
{
  readonly FindModel<PhoneNumber, long?> FindPhoneNumber = Substitute.For<FindModel<PhoneNumber, long?>>();
  readonly SaveModels<Contact, PhoneNumber> SaveContactAndPhoneNumber = Substitute.For<SaveModels<Contact, PhoneNumber>>();

  [TestMethod]
  public async Task contact_and_new_phone_number__add_phone_number_to_contact__contact_and_phone_number_saved ()
  {
    var contact = CreateTestContact();
    var phoneNumber = CreateTestPhoneNumber();

    var saveContactAndPhoneNumber = Substitute.For<SaveModels<Contact, PhoneNumber>>();
    var result = await AddContactPhoneNumberService(contact, phoneNumber, FindPhoneNumber, saveContactAndPhoneNumber);

    await saveContactAndPhoneNumber.Received().Invoke(
      Arg.Is<Contact>(ct => ct.ContactId == contact.ContactId),
      Arg.Is<PhoneNumber>(pn => pn == phoneNumber)
    );
  }

  [TestMethod]
  public async Task contact_with_existing_phone_number__add_same_phone_number_to_contact__duplicate_phone_number_error ()
  {
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);
    var findPhoneNumber = Substitute.For<FindModel<PhoneNumber, long?>>();

    findPhoneNumber(phoneNumber).Returns((_) => FromResult((long?)phoneNumber.Number));
    var result = await AddContactPhoneNumberService(contact, phoneNumber, findPhoneNumber, SaveContactAndPhoneNumber);

    AreEqual(FromFailure(result)!, [GetDuplicatePhoneNumberError(phoneNumber.Number)]);
  }

  [TestMethod]
  public async Task phone_number_with_invalid_number__add_phone_number_to_contact__invalid_phone_number_error ()
  {
    var contact = CreateTestContact();
    var phoneNumber = CreateTestPhoneNumber(number: 100_000_000_000);
    var result = await AddContactPhoneNumberService(contact, phoneNumber, FindPhoneNumber, SaveContactAndPhoneNumber);

    AreEqual(FromFailure(result)!, [GetInvalidPhoneNumberError(phoneNumber.Number)]);
  }
}