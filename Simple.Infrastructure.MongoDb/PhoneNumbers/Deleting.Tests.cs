
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbTests
{
 [TestMethod]
  public async Task contact_with_phone_numbers__delete_phone_number__phone_number_deleted_from_contact ()
  {
    var contacts = GetContactCollection(Database);
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);

    await InsertContact(contacts, contact);
    await DeleteContactPhoneNumber(contacts, contact, phoneNumber);

    var actual = await FindContactByKey(contacts.AsQueryable(), contact.ContactId).SingleAsync();
    AreEqual(actual.PhoneNumbers, []);
  }
}