
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
 [TestMethod]
  public async Task contacts__get_contact_by_id__contact_stored ()
  {
    using var dbContext = CreateAgendaContext();
    var contact = CreateTestContact();

    AddContact(dbContext, contact);
    await SaveChangesAndClearContext(dbContext);

    var actual = await FindContactById (dbContext.Contacts, contact.ContactId).SingleAsync();
    Assert.IsNotNull(actual);
  }

 [TestMethod]
  public async Task contacts__get_contact_by_name__contact_stored ()
  {
    using var dbContext = CreateAgendaContext();
    var contact = CreateTestContact();

    AddContact(dbContext, contact);
    await SaveChangesAndClearContext(dbContext);

    var actual = await FindContactByName (dbContext.Contacts, contact.ContactName).SingleAsync();
    Assert.IsNotNull(actual);
  }
}