
using System.Net.Http.Json;

namespace Simple.Web.Testing;

partial class TestingFuncs
{
  [TestMethod]
  public async Task contact_with_phone_number__create_mongo_contact__contact_with_phone_number_created_and_notification_sent ()
  {
    using var apiClient = ApiServer.GetTestClient();
    var apiPathBase = GetApiPathBase(ApiServer);
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);

    using var contactJson = JsonContent.Create(contact);
    var contactCreateResponse = await apiClient.PostAsync(new Uri(apiPathBase + "/mongo/contacts"), contactJson);
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