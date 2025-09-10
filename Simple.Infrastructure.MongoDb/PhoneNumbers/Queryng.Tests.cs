
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbTests
{
  [TestMethod]
  public async Task phone_numbers__find_phone_number__filtered_phone_number()
  {
    var contacts = GetContactCollection(MongoDatabase);

    var contact1 = CreateTestContact(phoneNumbers: [CreateTestPhoneNumber()]);
    var contact2 = CreateTestContact(phoneNumbers: [CreateTestPhoneNumber()]);

    await InsertContactAsync(contacts, contact1);
    await InsertContactAsync(contacts, contact2);

    var actual = await FindContactPhoneNumber(contacts.AsQueryable(), contact1.PhoneNumbers[0]).ToListAsync();
    AreEqual(actual, [contact1]);
  }
}