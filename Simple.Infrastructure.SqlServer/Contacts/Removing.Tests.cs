
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
 [TestMethod]
  public async Task contact_with_phone_numbers__delete_phone_number__phone_number_deleted ()
  {
    using var dbContext1 = CreateAgendaContext();
    var contact = CreateTestContact();
    var phoneNumber = CreateTestPhoneNumber();

    AddContact(dbContext1, contact, phoneNumber);
    await dbContext1.SaveChangesAsync();

    using var dbContext2 = CreateAgendaContext();
    DeleteContactPhoneNumber(dbContext2, contact, phoneNumber);
    await dbContext2.SaveChangesAsync();

    using var dbContext3 = CreateAgendaContext();
    var actual = await GetContactById (dbContext3.Contacts.Include(e => e.PhoneNumbers), contact.ContactId).SingleAsync();
    AreEqual(actual.PhoneNumbers!, []);
  }
}