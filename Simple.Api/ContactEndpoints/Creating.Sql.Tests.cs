#pragma warning disable CA1305

using System.Net.Http;

namespace Simple.Api;

partial class ApiTesting
{
  [TestMethod]
  public async Task new_contact_with_phone_number__create_sql_contact__contact_with_phone_number_created ()
  {
    using var apiClient = ApiServer.GetTestClient();
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
    var contactCreateResponse = await apiClient.PostAsync("/sql/contacts", contactForm);
    await EnsureResponseMessageSuccess(contactCreateResponse);

    var contactResponse = await apiClient.GetAsync(GetResponseMessageLocation(contactCreateResponse));
    await EnsureResponseMessageSuccess(contactResponse);

    var actual = await ReadResponseMessageJsonContent<Contact>(contactResponse);
    Assert.AreEqual(actual!.ContactName, contact.ContactName);
    Assert.AreEqual(actual!.PhoneNumbers[0], phoneNumber);
  }

  [TestMethod]
  public async Task new_contact__create_sql_contact__added_to_agenda_notification_sent ()
  {
    using var apiClient = ApiServer.GetTestClient();
    var contact = CreateTestContact();

    using var contactForm = new FormUrlEncodedContent([
      new KeyValuePair<string, string>("contactName", contact.ContactName),
      new KeyValuePair<string, string>("contactEmail", contact.ContactEmail)
    ]);
    var contactCreateResponse = await apiClient.PostAsync("/sql/contacts", contactForm);
    await EnsureResponseMessageSuccess(contactCreateResponse);

    var actual = await ReceiveNotificationsAsync<Notification>(contact.ContactEmail, contact.ContactEmail, GetEmailServerOptions(Configuration), notification => notification.Title == AddedToAgendaTitle);
    Assert.IsNotNull(actual);
  }
}