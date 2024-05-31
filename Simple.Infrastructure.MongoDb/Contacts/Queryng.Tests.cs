
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbTests
{
 [TestMethod]
  public async Task contacts__find_contact_by_key__stored_contact_with_id ()
  {
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [ phoneNumber ]);

    var db = GetMongoDatabase();
    var contactColl = GetContactCollection(db);
    await InsertDocument(contactColl, contact);

    var actual = await FindContactByKey(contactColl.AsQueryable(), contact.ContactId).FirstOrDefaultAsync();
    Assert.AreEqual(actual, contact);
  }

 [TestMethod]
  public async Task contacts__get_contact_by_name__stored_contact_with_name ()
  {
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [ phoneNumber ]);

    var db = GetMongoDatabase();
    var contactColl = GetContactCollection(db);
    await InsertDocument(contactColl, contact);

    var actual = await FindContactByKey(contactColl.AsQueryable(), contact.ContactId).FirstOrDefaultAsync();
    Assert.AreEqual(actual, contact);
  }
}