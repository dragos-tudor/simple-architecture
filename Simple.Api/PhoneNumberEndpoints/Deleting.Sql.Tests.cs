
using Microsoft.EntityFrameworkCore;

namespace Simple.Api;

partial class ApiTesting
{
  [TestMethod]
  public async Task contact_with_phone_number__delete_phone_number_from_sql_contact__phone_number_deleted_from_contact ()
  {
    using var apiClient = ApiServer.GetTestClient();
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);

    using var agendaContext = await AgendaContextFactory.CreateDbContextAsync();
    await InsertContact(agendaContext, contact);

    var phoneNumberDeletedResponse = await apiClient.DeleteAsync(GetSqlPhoneNumberCreatedPath(contact.ContactId, phoneNumber.CountryCode, phoneNumber.Number));
    phoneNumberDeletedResponse.EnsureSuccessStatusCode();

    var actual = await FindContactByKey(agendaContext.Contacts.AsQueryable(), contact.ContactId).Include(c => c.PhoneNumbers).FirstOrDefaultAsync();
    AreEqual(actual!.PhoneNumbers, []);
  }
}