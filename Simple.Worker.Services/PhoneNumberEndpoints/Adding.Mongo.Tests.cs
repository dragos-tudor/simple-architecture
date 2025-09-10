
using MongoDB.Driver;

namespace Simple.Domain.Integrations;

partial class IntegrationsTests
{
  [TestMethod]
  public async Task new_phone_number__add_phone_number_to_mongo_contact__phone_number_added_to_contact ()
  {
    var contact = CreateTestContact();
    var phoneNumber = CreateTestPhoneNumber();

    var contacts = GetContactCollection(MongoDatabase);
    await InsertContactAsync(contacts, contact);

    var response = await AddPhoneNumberMongoEndpoint(contact.ContactId, phoneNumber, MongoDatabase, new DefaultHttpContext(), Logger);

    var actual = await FindContactByKey(contacts.AsQueryable(), contact.ContactId).FirstOrDefaultAsync();
    AreEqual(actual!.PhoneNumbers, [phoneNumber]);
    Assert.IsInstanceOfType(response.Result, typeof(Created));
  }
}