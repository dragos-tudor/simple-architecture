
using Microsoft.EntityFrameworkCore;

namespace Simple.Domain.Integrations;

partial class IntegrationsTests
{
  [TestMethod]
  public async Task contact_with_phone_number__delete_phone_number_from_sql_contact__phone_number_deleted_from_contact ()
  {
    using var dbContext = await SqlContextFactory.CreateDbContextAsync();
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);

    await InsertContactAsync(dbContext, contact);

    await DeletePhoneNumberSqlEndpoint(contact.ContactId, phoneNumber.CountryCode, phoneNumber.Number, SqlContextFactory, new DefaultHttpContext(), Logger);

    var actual = await FindContactByKey(dbContext.Contacts.Include(c => c.PhoneNumbers).AsQueryable(), contact.ContactId).FirstOrDefaultAsync();
    AreEqual(actual!.PhoneNumbers, []);
  }
}