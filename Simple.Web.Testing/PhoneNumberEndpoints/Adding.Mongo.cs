
using System.Net.Http.Json;

namespace Simple.Web.Testing;

partial class TestingFuncs
{
  [TestMethod]
  public async Task new_phone_number__add_phone_number_to_mongo_contact__phone_number_added_to_contact ()
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

    var contactResponse = await apiClient.GetAsync(new Uri(apiPathBase + GetResponseMessageLocation(contactCreatedResponse)));
    contactResponse.EnsureSuccessStatusCode();

    var actual = await ReadResponseMessageJsonContent<Contact>(contactResponse);
    AreEqual(actual!.PhoneNumbers, [phoneNumber]);
  }
}