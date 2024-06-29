
namespace Simple.Domain.Integrations;

partial class IntegrationsTests
{
  [TestMethod]
  public async Task contact__find_mongo_contact__found_contact ()
  {
    var contact = CreateTestContact();

    var contacts = GetContactCollection(MongoDatabase);
    await InsertContactAsync(contacts, contact);

    var actual = await FindContactMongoEndpoint(contact.ContactId, MongoDatabase, new DefaultHttpContext());
    Assert.AreEqual(actual.Value, contact);
    Assert.AreEqual(actual.StatusCode, 200);
  }
}