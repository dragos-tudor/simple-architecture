
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
 [TestMethod]
  public async Task contacts__find_contact_by_key__stored_contact_with_id ()
  {
    using var dbContext = CreateAgendaContext(AgendaConnString);
    var contact = CreateTestContact();

    await InsertContact(dbContext, contact);
    ClearChangeTracker(dbContext);

    var actual = await FindContactByKey (dbContext.Contacts.AsQueryable(), contact.ContactId).SingleAsync();
    Assert.IsNotNull(actual);
  }

 [TestMethod]
  public async Task contacts__find_contact_by_name__stored_contact_with_name ()
  {
    using var dbContext = CreateAgendaContext(AgendaConnString);
    var contact = CreateTestContact();

    await InsertContact(dbContext, contact);
    ClearChangeTracker(dbContext);

    var actual = await FindContactByName (dbContext.Contacts.AsQueryable(), contact.ContactName).SingleAsync();
    Assert.IsNotNull(actual);
  }

 [TestMethod]
  public async Task contacts__find_contact_by_email__stored_contact_with_email ()
  {
    using var dbContext = CreateAgendaContext(AgendaConnString);
    var contact = CreateTestContact();

    await InsertContact(dbContext, contact);
    ClearChangeTracker(dbContext);

    var actual = await FindContactByEmail (dbContext.Contacts.AsQueryable(), contact.ContactEmail).SingleAsync();
    Assert.IsNotNull(actual);
  }
}