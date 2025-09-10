
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
  [TestMethod]
  public async Task contact_without_phone_numbers__add_phone_number__phone_number_added_on_contact()
  {
    using var dbContext = CreateAgendaContext(SqlConnectionString);
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);

    AddContact(dbContext, contact);
    AddPhoneNumber(dbContext, phoneNumber);
    await SaveChangesAsync(dbContext);
    ClearChangeTracker(dbContext);

    var actual = await FindContactById(dbContext.Contacts.Include(e => e.PhoneNumbers).AsQueryable(), contact.ContactId).SingleAsync();
    Assert.AreEqual(actual.PhoneNumbers[0], contact.PhoneNumbers[0]);
  }

}