
namespace Simple.Api;

partial class ApiTests
{
  [TestMethod]
  public async Task contact_api_mongo_tests()
  {
    using var apiClient = ApiServer.GetTestClient();
    var contact = CreateTestContact();
    var phoneNumber = CreateTestPhoneNumber();
    using var contactJson = JsonContent.Create(contact);
    using var phoneNumberJson = JsonContent.Create(phoneNumber);

    var contactCreatedResponse = await apiClient.PostAsync("/mongo/contacts", contactJson);
    await EnsureHttpResponseMessageSuccessAsync(contactCreatedResponse);

    var phoneNumberCreatedResponse = await apiClient.PostAsync(GetHttpResponseMessageLocation(contactCreatedResponse) + "/phoneNumbers", phoneNumberJson);
    await EnsureHttpResponseMessageSuccessAsync(phoneNumberCreatedResponse);

    var phoneNumberDeletedResponse = await apiClient.DeleteAsync(GetHttpResponseMessageLocation(phoneNumberCreatedResponse));
    await EnsureHttpResponseMessageSuccessAsync(phoneNumberDeletedResponse);

    var contactResponse = await apiClient.GetAsync(GetHttpResponseMessageLocation(contactCreatedResponse));
    await EnsureHttpResponseMessageSuccessAsync(contactResponse);

    var actual = await ReadHttpResponseMessageJsonAsync<Contact>(contactResponse);
    Assert.AreEqual(actual!.ContactName, contact.ContactName);
  }
}