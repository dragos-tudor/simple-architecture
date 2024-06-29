
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbTests
{
 [TestMethod]
  public async Task contact_with_phone_numbers__delete_phone_number__phone_number_deleted_from_contact ()
  {
    var contacts = GetContactCollection(MongoDatabase);
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);

    await InsertContactAsync(contacts, contact);
    await DeletePhoneNumberAsync(contacts, contact, phoneNumber);

    var actual = await FindContactByKey(contacts.AsQueryable(), contact.ContactId).SingleAsync();
    AreEqual(actual.PhoneNumbers, []);
  }
}