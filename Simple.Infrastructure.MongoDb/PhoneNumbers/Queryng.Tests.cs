
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbTests
{
  [TestMethod]
  public async Task phone_numbers__find_phone_number__stored_phone_number ()
  {
    var contacts = GetContactCollection(MongoDatabase);

    var contact1 = CreateTestContact(phoneNumbers: [ CreateTestPhoneNumber() ]);
    var contact2 = CreateTestContact(phoneNumbers: [ CreateTestPhoneNumber() ]);

    await InsertContactAsync(contacts, contact1);
    await InsertContactAsync(contacts, contact2);

    var actual = await FindPhoneNumber(contacts.AsQueryable(), contact1.PhoneNumbers[0]);
    Assert.AreEqual(actual, contact1.PhoneNumbers[0]);
  }

  [TestMethod]
  public async Task phone_numbers__find_phone_numbers__stored_phone_numbers ()
  {
    var contacts = GetContactCollection(MongoDatabase);

    var contact1 = CreateTestContact(phoneNumbers: [ CreateTestPhoneNumber() ]);
    var contact2 = CreateTestContact(phoneNumbers: [ CreateTestPhoneNumber() ]);
    var contact3 = CreateTestContact(phoneNumbers: [ CreateTestPhoneNumber() ]);

    await InsertContactAsync(contacts, contact1);
    await InsertContactAsync(contacts, contact2);
    await InsertContactAsync(contacts, contact3);

    var actual = await FindPhoneNumbers(contacts.AsQueryable(), [contact1.PhoneNumbers[0], contact3.PhoneNumbers[0]]);
    CollectionAssert.AreEqual(actual.ToArray(), (PhoneNumber[])[contact1.PhoneNumbers[0], contact3.PhoneNumbers[0]]);
  }

}