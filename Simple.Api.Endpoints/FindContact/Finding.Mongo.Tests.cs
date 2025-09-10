
namespace Simple.Api.Endpoints;

partial class EndpointsTests
{
  [TestMethod]
  public async Task contact__find_mongo_contact__found_contact()
  {
    var contact = CreateTestContact();
    var contactColl = GetContactCollection(MongoDatabase);

    await InsertContactAsync(contactColl, contact);

    var result = await FindContactMongoAsync(contact.ContactId, MongoDatabase, CancellationToken.None);

    var httpContext = await ExecuteResultAsync(result, CreateHttpContext());
    Assert.AreEqual(contact, ReadHttpResponseJson<Contact>(httpContext.Response));
    Assert.AreEqual(200, GetHttpResponseStatusCode(httpContext.Response));
  }
}