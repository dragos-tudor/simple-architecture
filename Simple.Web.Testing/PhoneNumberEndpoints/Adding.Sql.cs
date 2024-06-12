#pragma warning disable CA1305

using System.Net.Http;

namespace Simple.Web.Testing;

partial class TestingFuncs
{
  [TestMethod]
  public async Task new_phone_number__add_phone_number_to_sql_contact__phone_number_added_to_contact ()
  {
    using var apiClient = ApiServer.GetTestClient();
    var apiPathBase = GetApiPathBase(ApiServer);
    var contact = CreateTestContact();
    var phoneNumber = CreateTestPhoneNumber();
    using var contactForm = new FormUrlEncodedContent([
      new KeyValuePair<string, string>("contactName", contact.ContactName),
      new KeyValuePair<string, string>("contactEmail", contact.ContactEmail)
    ]);
    using var phoneNumberForm = new FormUrlEncodedContent([
      new KeyValuePair<string, string>("countryCode", phoneNumber.CountryCode.ToString()),
      new KeyValuePair<string, string>("number", phoneNumber.Number.ToString()),
      new KeyValuePair<string, string>("numberType", phoneNumber.NumberType.ToString()),
      new KeyValuePair<string, string>("extension", phoneNumber.Extension.ToString()!)
    ]);

    var contactCreatedResponse = await apiClient.PostAsync(new Uri(apiPathBase + "/sql/contacts"), contactForm);
    contactCreatedResponse.EnsureSuccessStatusCode();

    var phoneNumberCreatedResponse = await apiClient.PostAsync(new Uri(apiPathBase + GetResponseMessageLocation(contactCreatedResponse) + "/phoneNumbers"), phoneNumberForm);
    phoneNumberCreatedResponse.EnsureSuccessStatusCode();

    var contactResponse = await apiClient.GetAsync(new Uri(apiPathBase + GetResponseMessageLocation(contactCreatedResponse)));
    contactResponse.EnsureSuccessStatusCode();

    var actual = await ReadResponseMessageJsonContent<Contact>(contactResponse);
    AreEqual(actual!.PhoneNumbers, [phoneNumber]);
  }
}