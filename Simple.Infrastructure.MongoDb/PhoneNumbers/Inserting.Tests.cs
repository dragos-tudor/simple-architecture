
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbTests
{
  [TestMethod]
  public async Task contact_without_phone_numbers__insert_phone_number__phone_number_added_on_contact()
  {
    var contacts = GetContactCollection(MongoDatabase);

    var contact = CreateTestContact();
    await InsertContactAsync(contacts, contact);

    var phoneNumber = CreateTestPhoneNumber();
    await InsertPhoneNumberAsync(contacts, contact, phoneNumber);

    var actual = await FindContactById(contacts.AsQueryable(), contact.ContactId).SingleAsync();
    Assert.AreEqual(actual.PhoneNumbers[0], phoneNumber);
  }

  [TestMethod]
  public async Task contact_with_phone_numbers__insert_phone_number__phone_number_added_on_contact()
  {
    var contacts = GetContactCollection(MongoDatabase);

    var contact = CreateTestContact(phoneNumbers: [CreateTestPhoneNumber()]);
    await InsertContactAsync(contacts, contact);

    var phoneNumber = CreateTestPhoneNumber();
    await InsertPhoneNumberAsync(contacts, contact, phoneNumber);

    var actual = await FindContactById(contacts.AsQueryable(), contact.ContactId).SingleAsync();
    Assert.AreEqual(actual.PhoneNumbers.Count, 2);
    Assert.AreEqual(actual.PhoneNumbers[1], phoneNumber);
  }
}