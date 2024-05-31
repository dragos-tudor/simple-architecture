
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
 [TestMethod]
  public async Task contacts__find_contact_by_key__stored_contact_with_id ()
  {
    using var dbContext = CreateAgendaContext();
    var contact = CreateTestContact();

    AddContact(dbContext, contact);
    await SaveChangesAndClearContext(dbContext);

    var actual = await FindContactById (dbContext.Contacts, contact.ContactId).SingleAsync();
    Assert.IsNotNull(actual);
  }

 [TestMethod]
  public async Task contacts__get_contact_by_name__stored_contact_with_name ()
  {
    using var dbContext = CreateAgendaContext();
    var contact = CreateTestContact();

    AddContact(dbContext, contact);
    await SaveChangesAndClearContext(dbContext);

    var actual = await FindContactByName (dbContext.Contacts, contact.ContactName).SingleAsync();
    Assert.IsNotNull(actual);
  }
}