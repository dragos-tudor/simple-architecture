
namespace Simple.Api;

partial class ApiTesting
{
  [TestMethod]
  public async Task new_phone_number__add_phone_number_to_mongo_contact__phone_number_added_to_contact ()
  {
    using var apiClient = ApiServer.GetTestClient();
    var contact = CreateTestContact();
    var phoneNumber = CreateTestPhoneNumber();

    var contacts = GetContactCollection(AgendaDatabase);
    await InsertContact(contacts, contact);

    using var phoneNumberJson = JsonContent.Create(phoneNumber);
    var phoneNumberCreatedResponse = await apiClient.PostAsync(GetMongoPhoneNumbersPath(contact.ContactId), phoneNumberJson);
    phoneNumberCreatedResponse.EnsureSuccessStatusCode();

    var actual = await FindContactByKey(contacts.AsQueryable(), contact.ContactId).FirstOrDefaultAsync();
    AreEqual(actual!.PhoneNumbers, [phoneNumber]);
  }
}