
using System.Net.Http;

namespace Simple.Web.Testing;

partial class TestingFuncs
{
  [TestMethod]
  public async Task contact__create_contact__contact_created_and_notification_sent ()
  {
    using var apiClient = ApiServer.GetTestClient();
    var apiPathBase = GetApiPathBase(ApiServer);
    var contact = CreateTestContact();
    using var contactForm = new FormUrlEncodedContent([
      new KeyValuePair<string, string>("contactName", contact.ContactName),
      new KeyValuePair<string, string>("contactEmail", contact.ContactEmail)
    ]);

    var createResponse = await apiClient.PostAsync(new Uri(apiPathBase + "/sql/contacts"), contactForm);
    var contactResponse = await apiClient.GetAsync(new Uri(apiPathBase + GetResponseMessageLocation(createResponse)));

    var actual = await ReadResponseMessageJsonContent<Contact>(contactResponse);
    Assert.AreEqual(actual!.ContactName, contact.ContactName);
    await WaitForNotification(contact.ContactEmail);
  }
}