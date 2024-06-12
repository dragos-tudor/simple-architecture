
using System.Net.Http.Json;

namespace Simple.Web.Testing;

partial class TestingFuncs
{
  [TestMethod]
  public async Task contact_with_phone_number__delete_phone_number_from_mongo__phone_number_deleted_from_contact ()
  {
    using var apiClient = ApiServer.GetTestClient();
    var apiPathBase = GetApiPathBase(ApiServer);
    var contact = CreateTestContact();
    var phoneNumber = CreateTestPhoneNumber();
    using var contactJson = JsonContent.Create(contact);
    using var phoneNumberJson = JsonContent.Create(phoneNumber);

    var contactCreatedResponse = await apiClient.PostAsync(new Uri(apiPathBase + "/mongo/contacts"), contactJson);
    contactCreatedResponse.EnsureSuccessStatusCode();

    var phoneNumberCreatedResponse = await apiClient.PostAsync(new Uri(apiPathBase + GetResponseMessageLocation(contactCreatedResponse) + "/phoneNumbers"), phoneNumberJson);
    phoneNumberCreatedResponse.EnsureSuccessStatusCode();

    var phoneNumberDeletedResponse = await apiClient.DeleteAsync(new Uri(apiPathBase + GetResponseMessageLocation(phoneNumberCreatedResponse)));
    phoneNumberDeletedResponse.EnsureSuccessStatusCode();

    var contactResponse = await apiClient.GetAsync(new Uri(apiPathBase + GetResponseMessageLocation(contactCreatedResponse)));
    contactResponse.EnsureSuccessStatusCode();

    var actual = await ReadResponseMessageJsonContent<Contact>(contactResponse);
    // AreEqual(actual!.PhoneNumbers, []);
  }
}