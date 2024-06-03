
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
 [TestMethod]
  public async Task contact_with_phone_numbers__delete_phone_number__phone_number_deleted_from_contact ()
  {
    using var dbContext = CreateAgendaContext(AgendaConnString);
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);

    AddContact(dbContext, contact);
    await SaveTestChanges(dbContext);

    DeletePhoneNumber(dbContext, ClonePhoneNumber(phoneNumber));
    await SaveTestChanges(dbContext);

    var actual = await FindContactByKey (dbContext.Contacts.Include(e => e.PhoneNumbers), contact.ContactId).SingleAsync();
    AreEqual(actual.PhoneNumbers, []);
  }
}