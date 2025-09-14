
namespace Simple.Api;

partial class ApiTests
{
  [TestMethod]
  public async Task api_mongo_tests()
  {
    using var apiClient = ApiServer.GetTestClient();
    var contact = CreateTestContact();
    var phoneNumber = CreateTestPhoneNumber();
    using var contactJson = JsonContent.Create(contact);
    using var phoneNumberJson = JsonContent.Create(phoneNumber);

    var contactCreatedResponse = await apiClient.PostAsync("/mongo/contacts", contactJson);
    await EnsureHttpResponseStatusCodeAsync(contactCreatedResponse, HttpStatusCode.Created);

    var phoneNumberCreatedResponse = await apiClient.PostAsync(GetHttpResponseMessageLocation(contactCreatedResponse) + "/phoneNumbers", phoneNumberJson);
    await EnsureHttpResponseStatusCodeAsync(phoneNumberCreatedResponse, HttpStatusCode.Created);

    var phoneNumberDeletedResponse = await apiClient.DeleteAsync(GetHttpResponseMessageLocation(phoneNumberCreatedResponse));
    await EnsureHttpResponseSuccessAsync(phoneNumberDeletedResponse);

    var contactResponse = await apiClient.GetAsync(GetHttpResponseMessageLocation(contactCreatedResponse));
    await EnsureHttpResponseSuccessAsync(contactResponse);

    var actual = await ReadHttpResponseMessageJsonAsync<Contact>(contactResponse);
    Assert.AreEqual(actual!.ContactName, contact.ContactName);
    AreEqual(actual.PhoneNumbers, []);
  }
}