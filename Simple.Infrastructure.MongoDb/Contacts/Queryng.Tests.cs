
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbTests
{
 [TestMethod]
  public async Task contacts__find_contact_by_key__stored_contact_with_id ()
  {
    var contacts = GetContactCollection(Database);

    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [ phoneNumber ]);

    await InsertContact(contacts, contact);

    var actual = await FindContactByKey(contacts.AsQueryable(), contact.ContactId).FirstOrDefaultAsync();
    Assert.AreEqual(actual, contact);
  }

 [TestMethod]
  public async Task contacts__get_contact_by_name__stored_contact_with_name ()
  {
    var contacts = GetContactCollection(Database);

    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [ phoneNumber ]);

    await InsertContact(contacts, contact);

    var actual = await FindContactByName(contacts.AsQueryable(), contact.ContactName).FirstOrDefaultAsync();
    Assert.AreEqual(actual, contact);
  }

 [TestMethod]
  public async Task contacts__get_contact_by_email__stored_contact_with_email ()
  {
    var contacts = GetContactCollection(Database);

    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [ phoneNumber ]);

    await InsertContact(contacts, contact);

    var actual = await FindContactByEmail(contacts.AsQueryable(), contact.ContactEmail).FirstOrDefaultAsync();
    Assert.AreEqual(actual, contact);
  }
}