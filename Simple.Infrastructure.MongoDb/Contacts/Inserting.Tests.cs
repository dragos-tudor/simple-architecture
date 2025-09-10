
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbTests
{
  [TestMethod]
  public async Task new_contact__insert_contact__contact_stored()
  {
    var contacts = GetContactCollection(MongoDatabase);

    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);
    await InsertContactAsync(contacts, contact);

    Assert.IsNotNull(await FindContactById(contacts.AsQueryable(), contact.ContactId).FirstOrDefaultAsync());
  }
}