
namespace Simple.Api;

partial class ApiTests
{
  [TestMethod]
  public async Task contact_api_integration_mongo_tests ()
  {
    using var apiClient = ApiServer.GetTestClient();
    var contact = CreateTestContact();
    var phoneNumber = CreateTestPhoneNumber();
    using var contactJson = JsonContent.Create(contact);
    using var phoneNumberJson = JsonContent.Create(phoneNumber);

    var contactCreatedResponse = await apiClient.PostAsync("/mongo/contacts", contactJson);
    await EnsureResponseMessageSuccess(contactCreatedResponse);

    var phoneNumberCreatedResponse = await apiClient.PostAsync(GetResponseMessageLocation(contactCreatedResponse) + "/phoneNumbers", phoneNumberJson);
    await EnsureResponseMessageSuccess(phoneNumberCreatedResponse);

    var phoneNumberDeletedResponse = await apiClient.DeleteAsync(GetResponseMessageLocation(phoneNumberCreatedResponse));
    await EnsureResponseMessageSuccess(phoneNumberDeletedResponse);

    var contactResponse = await apiClient.GetAsync(GetResponseMessageLocation(contactCreatedResponse));
    await EnsureResponseMessageSuccess(contactResponse);

    var actual = await ReadResponseMessageJsonContent<Contact>(contactResponse);
    Assert.AreEqual(actual!.ContactName, contact.ContactName);
  }
}