#pragma warning disable CA1305

using System.Net.Http;

namespace Simple.Api.Testing;

partial class TestingFuncs
{
  [TestMethod]
  public async Task contact_with_phone_number__create_sql_contact__contact_with_phone_number_created_and_notification_sent ()
  {
    using var apiClient = ApiServer.GetTestClient();
    var apiPathBase = GetApiPathBase(ApiServer);
    var contact = CreateTestContact();
    var phoneNumber = CreateTestPhoneNumber();

    using var contactForm = new FormUrlEncodedContent([
      new KeyValuePair<string, string>("contactName", contact.ContactName),
      new KeyValuePair<string, string>("contactEmail", contact.ContactEmail),
      new KeyValuePair<string, string>("phoneNumbers[0].countryCode", phoneNumber.CountryCode.ToString()),
      new KeyValuePair<string, string>("phoneNumbers[0].number", phoneNumber.Number.ToString()),
      new KeyValuePair<string, string>("phoneNumbers[0].numberType", phoneNumber.NumberType.ToString()),
      new KeyValuePair<string, string>("phoneNumbers[0].extension", phoneNumber.Extension.ToString()!)
    ]);
    var contactCreateResponse = await apiClient.PostAsync(new Uri(apiPathBase + "/sql/contacts"), contactForm);
    contactCreateResponse.EnsureSuccessStatusCode();

    var contactResponse = await apiClient.GetAsync(new Uri(apiPathBase + GetResponseMessageLocation(contactCreateResponse)));
    contactResponse.EnsureSuccessStatusCode();

    var actual = await ReadResponseMessageJsonContent<Contact>(contactResponse);
    Assert.AreEqual(actual!.ContactName, contact.ContactName);
    Assert.AreEqual(actual!.PhoneNumbers[0], phoneNumber);

    var notifications = await ReceiveNotifications(contact.ContactEmail, contact.ContactEmail, notification => notification.Title == AddedToAgendaTitle);
    Assert.IsTrue(notifications.Any());
  }
}