
using Microsoft.EntityFrameworkCore;

namespace Simple.Api.Endpoints;

partial class EndpointsTests
{
  [TestMethod]
  public async Task contact_with_phone_number__delete_sql_phone_number__phone_number_deleted()
  {
    using var dbContext = CreateAgendaContext(SqlConnectionString);
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact();

    await InsertContactSqlAsync(dbContext, contact);
    await InsertPhoneNumberSqlAsync(dbContext, contact, phoneNumber);
    ClearChangeTracker(dbContext);

    await DeletePhoneNumberSqlAsync(contact.ContactId, phoneNumber.CountryCode, phoneNumber.Number, SqlConnectionString, CancellationToken.None);

    var actual = await FindContactById(dbContext.Contacts.Include(c => c.PhoneNumbers).AsQueryable(), contact.ContactId).FirstOrDefaultAsync();
    AreEqual(actual!.PhoneNumbers, []);
  }
}