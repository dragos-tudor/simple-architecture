
using static System.Threading.Tasks.Task;
using NSubstitute;

namespace Simple.Domain.Api;

partial class ApiTests
{
  readonly FindModel<PhoneNumber, PhoneNumber?> FindPhoneNumber = Substitute.For<FindModel<PhoneNumber, PhoneNumber?>>();
  readonly SaveModels<Contact, PhoneNumber> SaveContactAndPhoneNumber = Substitute.For<SaveModels<Contact, PhoneNumber>>();

  [TestMethod]
  public async Task contact_and_new_phone_number__add_phone_number_to_contact__contact_and_phone_number_saved ()
  {
    Contact contact = CreateTestContact();
    var phoneNumber = CreateTestPhoneNumber();
    var saveContactAndPhoneNumber = Substitute.For<SaveModels<Contact, PhoneNumber>>();
    var findContact = Substitute.For<FindModel<Guid, Contact?>>();

    findContact(contact.ContactId).Returns((_) => FromResult(contact) as Task<Contact?>);
    await AddPhoneNumberApi(contact.ContactId, phoneNumber, FindPhoneNumber, findContact, saveContactAndPhoneNumber);

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
    var findPhoneNumber = Substitute.For<FindModel<PhoneNumber, PhoneNumber?>>();
    var findContact = Substitute.For<FindModel<Guid, Contact?>>();

    findPhoneNumber(phoneNumber).Returns((_) => FromResult(phoneNumber) as Task<PhoneNumber?>);
    findContact(contact.ContactId).Returns((_) => FromResult(contact) as Task<Contact?>);
    var result = await AddPhoneNumberApi(contact.ContactId, phoneNumber, findPhoneNumber, findContact, SaveContactAndPhoneNumber);

    CollectionAssert.AreEqual(FromFailure(result)!, AsArray([GetDuplicatePhoneNumberError(phoneNumber)]));
  }

  [TestMethod]
  public async Task phone_number_with_invalid_number__add_phone_number_to_contact__invalid_phone_number_error ()
  {
    var contact = CreateTestContact();
    var phoneNumber = CreateTestPhoneNumber(number: 100_000_000_000);
    var findContact = Substitute.For<FindModel<Guid, Contact?>>();

    findContact(contact.ContactId).Returns((_) => FromResult(contact) as Task<Contact?>);
    var result = await AddPhoneNumberApi(contact.ContactId, phoneNumber, FindPhoneNumber, findContact, SaveContactAndPhoneNumber);

    CollectionAssert.AreEqual(FromFailure(result)!, AsArray([GetInvalidPhoneNumberError(phoneNumber.Number)]));
  }
}