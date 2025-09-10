
using MongoDB.Driver;

namespace Simple.Domain.Integrations;

partial class IntegrationsTests
{
  [TestMethod]
  public async Task contact_with_phone_number__delete_phone_number_from_mongo__phone_number_deleted_from_contact ()
  {
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);

    var contacts = GetContactCollection(MongoDatabase);
    await InsertContactAsync(contacts, contact);

    await DeletePhoneNumberMongoEndpoint(contact.ContactId, phoneNumber.CountryCode, phoneNumber.Number, MongoDatabase, new DefaultHttpContext(), Logger);

    var actual = await FindContactByKey(contacts.AsQueryable(), contact.ContactId).FirstOrDefaultAsync();
    AreEqual(actual!.PhoneNumbers, []);
  }
}