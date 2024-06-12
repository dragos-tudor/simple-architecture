
using static System.Threading.Tasks.Task;
using NSubstitute;

namespace Simple.Domain.Services;

partial class ServicesTests
{
  readonly SaveModels<Contact, PhoneNumber> DeletePhoneNumber = Substitute.For<SaveModels<Contact, PhoneNumber>>();

  [TestMethod]
  public async Task contact_with_phone_number__delete_phone_number_from_contact__phone_number_deleted ()
  {
    var contact = CreateTestContact();
    var phoneNumber = CreateTestPhoneNumber();
    var deletePhoneNumber = Substitute.For<SaveModels<Contact, PhoneNumber>>();
    var findContact = Substitute.For<FindModel<Guid, Contact?>>();

    findContact(contact.ContactId).Returns((_) => FromResult(contact) as Task<Contact?>);
    var result= await DeletePhoneNumberService(contact.ContactId, phoneNumber, findContact, deletePhoneNumber, Logger);

    await deletePhoneNumber.Received().Invoke(
      Arg.Is(contact),
      Arg.Is<PhoneNumber>(pn => pn == phoneNumber)
    );
  }

  [TestMethod]
  public async Task non_existing_contact__delete_phone_number_from_contact__missing_contact_error ()
  {
    var contactId = Guid.NewGuid();
    var phoneNumber = CreateTestPhoneNumber();
    var findContact = Substitute.For<FindModel<Guid, Contact?>>();

    findContact(contactId).Returns((_) => FromResult(default(Contact)));
    var result= await DeletePhoneNumberService(contactId, phoneNumber, findContact, DeletePhoneNumber, Logger);

    AreEqual(FromFailure(result)!, [GetMissingContactFailure(contactId)]);
  }
}