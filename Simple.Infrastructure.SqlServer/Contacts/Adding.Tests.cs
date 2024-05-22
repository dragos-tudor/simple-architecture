
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
 [TestMethod]
  public async Task contact__add_contact__contact_stored ()
  {
    using var dbContext1 = CreateAgendaContext();
    var contact = CreateTestContact();

    AddContact(dbContext1, contact);
    await dbContext1.SaveChangesAsync();

    using var dbContext2 = CreateAgendaContext();
    var actual = await GetContactById (dbContext2.Contacts, contact.ContactId).SingleAsync();
    actual.PhoneNumbers = [];
    Assert.AreEqual(actual, contact);
  }

 [TestMethod]
  public async Task contact_without_phone_numbers__add_phone_number__phone_number_added_on_contact ()
  {
    using var dbContext1 = CreateAgendaContext();
    var contact = CreateTestContact();

    AddContact(dbContext1, contact);
    await dbContext1.SaveChangesAsync();

    using var dbContext2 = CreateAgendaContext();
    AddContactPhoneNumber(dbContext2, contact, CreateTestPhoneNumber());
    await dbContext2.SaveChangesAsync();

    using var dbContext3 = CreateAgendaContext();
    var actual = await GetContactById (dbContext3.Contacts.Include(e => e.PhoneNumbers), contact.ContactId).SingleAsync();

    AreEqual(actual.PhoneNumbers!, contact.PhoneNumbers);
  }

 [TestMethod]
  public async Task contact_with_phone_numbers__add_phone_number__phone_number_added_on_contact ()
  {
    using var dbContext1 = CreateAgendaContext();
    var contact = CreateTestContact();

    AddContact(dbContext1, contact, CreateTestPhoneNumber());
    await dbContext1.SaveChangesAsync();

    using var dbContext2 = CreateAgendaContext();
    AddContactPhoneNumber(dbContext2, contact, CreateTestPhoneNumber());
    await dbContext2.SaveChangesAsync();

    using var dbContext3 = CreateAgendaContext();
    var actual = await GetContactById (dbContext3.Contacts.Include(e => e.PhoneNumbers), contact.ContactId).SingleAsync();
    AreEqual(OrderPhoneNumbers(actual.PhoneNumbers!), OrderPhoneNumbers(contact.PhoneNumbers!));
  }
}