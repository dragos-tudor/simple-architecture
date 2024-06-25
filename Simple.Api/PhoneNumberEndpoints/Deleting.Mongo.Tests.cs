
namespace Simple.Api;

partial class ApiTesting
{
  [TestMethod]
  public async Task contact_with_phone_number__delete_phone_number_from_mongo__phone_number_deleted_from_contact ()
  {
    using var apiClient = ApiServer.GetTestClient();
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);

    var contacts = GetContactCollection(AgendaDatabase);
    await InsertContact(contacts, contact);

    var phoneNumberDeletedResponse = await apiClient.DeleteAsync(GetMongoPhoneNumberCreatedPath(contact.ContactId, phoneNumber.CountryCode, phoneNumber.Number));
    await EnsureResponseMessageSuccess(phoneNumberDeletedResponse);

    var actual = await FindContactByKey(contacts.AsQueryable(), contact.ContactId).FirstOrDefaultAsync();
    AreEqual(actual!.PhoneNumbers, []);
  }
}