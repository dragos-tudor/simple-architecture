#pragma warning disable CA1305

using System.Net.Http;
using Microsoft.EntityFrameworkCore;

namespace Simple.Api;

partial class ApiTesting
{
  [TestMethod]
  public async Task new_phone_number__add_phone_number_to_sql_contact__phone_number_added_to_contact ()
  {
    using var apiClient = ApiServer.GetTestClient();
    var contact = CreateTestContact();
    var phoneNumber = CreateTestPhoneNumber();

    using var agendaContext = await AgendaContextFactory.CreateDbContextAsync();
    await InsertContact(agendaContext, contact);

    using var phoneNumberForm = new FormUrlEncodedContent([
      new KeyValuePair<string, string>("countryCode", phoneNumber.CountryCode.ToString()),
      new KeyValuePair<string, string>("number", phoneNumber.Number.ToString()),
      new KeyValuePair<string, string>("numberType", phoneNumber.NumberType.ToString()),
      new KeyValuePair<string, string>("extension", phoneNumber.Extension.ToString()!)
    ]);
    var phoneNumberCreatedResponse = await apiClient.PostAsync(GetSqlPhoneNumbersPath(contact.ContactId), phoneNumberForm);
    phoneNumberCreatedResponse.EnsureSuccessStatusCode();

    var actual = await FindContactByKey(agendaContext.Contacts.AsQueryable(), contact.ContactId).Include(c => c.PhoneNumbers).FirstOrDefaultAsync();
    AreEqual(actual!.PhoneNumbers, [phoneNumber]);
  }
}