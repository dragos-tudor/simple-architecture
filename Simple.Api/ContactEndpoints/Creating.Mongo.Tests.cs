
namespace Simple.Api;

partial class ApiTesting
{
  [TestMethod]
  public async Task new_contact_with_phone_number__create_mongo_contact__contact_with_phone_number_created ()
  {
    using var apiClient = ApiServer.GetTestClient();
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);

    using var contactJson = JsonContent.Create(contact);
    var contactCreateResponse = await apiClient.PostAsync("/mongo/contacts", contactJson);
    await EnsureResponseMessageSuccess(contactCreateResponse);

    var contactResponse = await apiClient.GetAsync(GetResponseMessageLocation(contactCreateResponse));
    await EnsureResponseMessageSuccess(contactResponse);

    var actual = await ReadResponseMessageJsonContent<Contact>(contactResponse);
    Assert.AreEqual(actual!.ContactName, contact.ContactName);
    Assert.AreEqual(actual!.PhoneNumbers[0], phoneNumber);
  }

  [TestMethod]
  public async Task new_contact__create_mongo_contact__added_to_agenda_notification_sent ()
  {
    using var apiClient = ApiServer.GetTestClient();
    var contact = CreateTestContact();

    using var contactJson = JsonContent.Create(contact);
    var contactCreateResponse = await apiClient.PostAsync("/mongo/contacts", contactJson);
    await EnsureResponseMessageSuccess(contactCreateResponse);

    var actual = await ReceiveNotificationsAsync<Notification>(contact.ContactEmail, contact.ContactEmail, GetEmailServerOptions(Configuration), notification => notification.Title == AddedToAgendaTitle);
    Assert.IsNotNull(actual);
  }
}