
using static System.Threading.Tasks.Task;
using NSubstitute;

namespace Simple.Domain.Services;

partial class ServicesTests
{
  readonly FindModel<PhoneNumber, PhoneNumber?> FindPhoneNumber = Substitute.For<FindModel<PhoneNumber, PhoneNumber?>>();
  readonly StoreModels<Contact, PhoneNumber> InsertPhoneNumber = Substitute.For<StoreModels<Contact, PhoneNumber>>();

  [TestMethod]
  public async Task contact_and_new_phone_number__add_phone_number_to_contact__phone_number_inserted()
  {
    var contact = CreateTestContact();
    var phoneNumber = CreateTestPhoneNumber();
    var insertPhoneNumber = Substitute.For<StoreModels<Contact, PhoneNumber>>();
    var findContact = Substitute.For<FindModel<Guid, Contact?>>();

    findContact(contact.ContactId).Returns((_) => FromResult(contact) as Task<Contact?>);
    await AddPhoneNumberAsync(contact.ContactId, phoneNumber, FindPhoneNumber, findContact, insertPhoneNumber);

    await insertPhoneNumber.Received().Invoke(
      Arg.Is<Contact>(ct => ct.ContactId == contact.ContactId),
      Arg.Is<PhoneNumber>(pn => pn == phoneNumber)
    );
  }

  [TestMethod]
  public async Task contact_with_existing_phone_number__add_same_phone_number_to_contact__duplicate_phone_number_error()
  {
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);
    var findPhoneNumber = Substitute.For<FindModel<PhoneNumber, PhoneNumber?>>();
    var findContact = Substitute.For<FindModel<Guid, Contact?>>();

    findPhoneNumber(phoneNumber).Returns((_) => FromResult(phoneNumber) as Task<PhoneNumber?>);
    findContact(contact.ContactId).Returns((_) => FromResult(contact) as Task<Contact?>);
    var (_, error) = await AddPhoneNumberAsync(contact.ContactId, phoneNumber, findPhoneNumber, findContact, InsertPhoneNumber);

    Assert.AreEqual(DuplicatePhoneNumberError, error!);
  }

  [TestMethod]
  public async Task phone_number_with_invalid_number__add_phone_number_to_contact__invalid_phone_number_error()
  {
    var contact = CreateTestContact();
    var phoneNumber = CreateTestPhoneNumber(number: 100_000_000_000);
    var findContact = Substitute.For<FindModel<Guid, Contact?>>();

    findContact(contact.ContactId).Returns((_) => FromResult(contact) as Task<Contact?>);
    var (_, error) = await AddPhoneNumberAsync(contact.ContactId, phoneNumber, FindPhoneNumber, findContact, InsertPhoneNumber);

    Assert.AreEqual(InvalidPhoneNumberError, error!);
  }

  [TestMethod]
  public async Task non_existing_contact__add_phone_number_to_contact__missing_contact_error()
  {
    var contactId = Guid.NewGuid();
    var phoneNumber = CreateTestPhoneNumber();
    var insertPhoneNumber = Substitute.For<StoreModels<Contact, PhoneNumber>>();
    var findContact = Substitute.For<FindModel<Guid, Contact?>>();

    findContact(contactId).Returns((_) => FromResult(default(Contact)));
    var (_, error) = await AddPhoneNumberAsync(contactId, phoneNumber, FindPhoneNumber, findContact, insertPhoneNumber);

    Assert.AreEqual(MissingContactError, error!);
  }
}