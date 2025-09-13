
using Microsoft.EntityFrameworkCore;

namespace Simple.Api.Endpoints;

partial class EndpointsTests
{
  [TestMethod]
  public async Task contact_and_phone_number__add_sql_phone_number_to_contact__phone_number_added_to_contact()
  {
    using var dbContext = CreateAgendaContext(SqlConnectionString);
    var contact = CreateTestContact();
    var request = CreateTestAddPhoneNumberRequest();

    await InsertContactSqlAsync(dbContext, contact);
    ClearChangeTracker(dbContext);

    var response = await AddPhoneNumberSqlAsync(contact.ContactId, request, SqlConnectionString, CancellationToken.None);
    ClearChangeTracker(dbContext);

    var actual = await FindContactById(dbContext.Contacts.Include(c => c.PhoneNumbers).AsQueryable(), contact.ContactId).FirstOrDefaultAsync();
    var phoneNumber = CreatePhoneNumber(request.CountryCode, request.Number, request.Extension, request.NumberType);

    AreEqual(actual!.PhoneNumbers, [phoneNumber]);
    Assert.IsInstanceOfType(response.Result, typeof(Created));
  }
}