#pragma warning disable CA1305

using System.Net.Http;

namespace Simple.Api;

partial class ApiTests
{
  [TestMethod]
  public async Task contact_api_sql_tests ()
  {
    using var apiClient = ApiServer.GetTestClient();
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

    var contactCreatedResponse = await apiClient.PostAsync("/sql/contacts", contactForm);
    await EnsureResponseMessageSuccess(contactCreatedResponse);

    var phoneNumberCreatedResponse = await apiClient.PostAsync(GetResponseMessageLocation(contactCreatedResponse) + "/phoneNumbers", phoneNumberForm);
    await EnsureResponseMessageSuccess(phoneNumberCreatedResponse);

    var phoneNumberDeletedResponse = await apiClient.DeleteAsync(GetResponseMessageLocation(phoneNumberCreatedResponse));
    await EnsureResponseMessageSuccess(phoneNumberDeletedResponse);

    var contactResponse = await apiClient.GetAsync(GetResponseMessageLocation(contactCreatedResponse));
    await EnsureResponseMessageSuccess(contactResponse);

    var actual = await ReadResponseMessageJsonContent<Contact>(contactResponse);
    Assert.AreEqual(actual!.ContactName, contact.ContactName);
  }
}