
using System.Net.Http.Json;
using MongoDB.Driver;

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
    using var phoneNumberJson = JsonContent.Create(phoneNumber);

    var contacts = GetContactCollection(AgendaDb);
    await InsertContact(contacts, contact);

    var phoneNumbersPath = apiPathBase + GetMongoPhoneNumbersPath(contact.ContactId);
    var phoneNumberCreatedResponse = await apiClient.PostAsync(new Uri(phoneNumbersPath), phoneNumberJson);
    phoneNumberCreatedResponse.EnsureSuccessStatusCode();

    var actual = await FindContactByKey(contacts.AsQueryable(), contact.ContactId).FirstOrDefaultAsync();
    AreEqual(actual!.PhoneNumbers, [phoneNumber]);
  }
}