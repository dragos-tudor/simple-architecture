
namespace Simple.Api;

partial class ApiTests
{
  [TestMethod]
  public async Task api_sql_tests()
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
    await EnsureHttpResponseStatusCodeAsync(contactCreatedResponse, HttpStatusCode.Created);

    var phoneNumberCreatedResponse = await apiClient.PostAsync(GetHttpResponseMessageLocation(contactCreatedResponse) + "/phoneNumbers", phoneNumberForm);
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