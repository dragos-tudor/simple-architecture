#pragma warning disable CA1305


using Microsoft.EntityFrameworkCore;

namespace Simple.Domain.Integrations;

partial class IntegrationsTests
{
  [TestMethod]
  public async Task new_phone_number__add_phone_number_to_sql_contact__phone_number_added_to_contact ()
  {
    using var dbContext = await SqlContextFactory.CreateDbContextAsync();
    var contact = CreateTestContact();
    var phoneNumber = CreateTestPhoneNumber();

    await InsertContactAsync(dbContext, contact);

    var response = await AddPhoneNumberSqlEndpoint(contact.ContactId, phoneNumber, SqlContextFactory, new DefaultHttpContext(), Logger);

    var actual = await FindContactByKey(dbContext.Contacts.Include(c => c.PhoneNumbers).AsQueryable(), contact.ContactId).FirstOrDefaultAsync();
    AreEqual(actual!.PhoneNumbers, [phoneNumber]);
    Assert.IsInstanceOfType(response.Result, typeof(Created));
  }
}