
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbTests
{
  [TestMethod]
  public async Task phone_numbers__find_phone_number__stored_phone_number ()
  {
    var client = GetMongoClient();
    var db = GetMongoDatabase(client);
    var contacts = GetContactCollection(db);

    var contact1 = CreateTestContact(phoneNumbers: [ CreateTestPhoneNumber() ]);
    var contact2 = CreateTestContact(phoneNumbers: [ CreateTestPhoneNumber() ]);

    await InsertContact(contacts, contact1);
    await InsertContact(contacts, contact2);

    var actual = await FindPhoneNumber(contacts.AsQueryable(), contact1.PhoneNumbers[0]);
    Assert.AreEqual(actual, contact1.PhoneNumbers[0]);
  }

  [TestMethod]
  public async Task phone_numbers__find_phone_numbers__stored_phone_numbers ()
  {
    var client = GetMongoClient();
    var db = GetMongoDatabase(client);
    var contacts = GetContactCollection(db);

    var contact1 = CreateTestContact(phoneNumbers: [ CreateTestPhoneNumber() ]);
    var contact2 = CreateTestContact(phoneNumbers: [ CreateTestPhoneNumber() ]);
    var contact3 = CreateTestContact(phoneNumbers: [ CreateTestPhoneNumber() ]);

    await InsertContact(contacts, contact1);
    await InsertContact(contacts, contact2);
    await InsertContact(contacts, contact3);

    var actual = await FindPhoneNumbers(contacts.AsQueryable(), [contact1.PhoneNumbers[0], contact3.PhoneNumbers[0]]);
    AreEqual(actual, [contact1.PhoneNumbers[0], contact3.PhoneNumbers[0]]);
  }

}