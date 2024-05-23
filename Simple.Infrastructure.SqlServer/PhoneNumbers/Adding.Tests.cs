
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
 [TestMethod]
  public async Task contact_without_phone_numbers__add_phone_number__phone_number_added_on_contact ()
  {
    using var dbContext = CreateAgendaContext();
    var contact = CreateTestContact();

    AddContact(dbContext, contact);
    await SaveChangesAndClearContext(dbContext);

    AddPhoneNumber(dbContext, contact, CreateTestPhoneNumber());
    await SaveChangesAndClearContext(dbContext);

    var actual = await FindContactById (dbContext.Contacts.Include(e => e.PhoneNumbers), contact.ContactId).SingleAsync();
    Assert.AreEqual(actual.PhoneNumbers[0], contact.PhoneNumbers[0]);
  }

 [TestMethod]
  public async Task contact_with_phone_numbers__add_phone_number__phone_number_added_on_contact ()
  {
    using var dbContext = CreateAgendaContext();
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);

    AddContact(dbContext, contact);
    await SaveChangesAndClearContext(dbContext);

    AddPhoneNumber(dbContext, contact, CreateTestPhoneNumber());
    await SaveChangesAndClearContext(dbContext);

    var actual = await FindContactById (dbContext.Contacts.Include(e => e.PhoneNumbers), contact.ContactId).SingleAsync();
    AreEqual(actual.PhoneNumbers.OrderBy(e => e.Number), contact.PhoneNumbers.OrderBy(e => e.Number));
  }
}