
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
 [TestMethod]
  public async Task new_contact__add_contact__contact_stored ()
  {
    using var dbContext = CreateAgendaContext();
    var contact = CreateTestContact();

    AddContact(dbContext, contact);
    await SaveChangesAndClearContext(dbContext);

    var actual = await FindContactByKey (dbContext.Contacts, contact.ContactId).SingleAsync();
    Assert.AreEqual(actual, contact);
  }
}