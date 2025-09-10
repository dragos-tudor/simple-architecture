
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
  [TestMethod]
  public async Task contacts__find_contact_by_id__contact_with_id()
  {
    using var dbContext = CreateAgendaContext(SqlConnectionString);
    var contact = CreateTestContact();

    AddContact(dbContext, contact);
    await SaveChangesAsync(dbContext);
    ClearChangeTracker(dbContext);

    var actual = await FindContactById(dbContext.Contacts.AsQueryable(), contact.ContactId).SingleAsync();
    Assert.IsNotNull(actual);
  }

  [TestMethod]
  public async Task contacts__find_contact_by_name__contact_with_name()
  {
    using var dbContext = CreateAgendaContext(SqlConnectionString);
    var contact = CreateTestContact();

    AddContact(dbContext, contact);
    await SaveChangesAsync(dbContext);
    ClearChangeTracker(dbContext);

    var actual = await FindContactByName(dbContext.Contacts.AsQueryable(), contact.ContactName).SingleAsync();
    Assert.IsNotNull(actual);
  }

  [TestMethod]
  public async Task contacts__find_contact_by_email__contact_with_email()
  {
    using var dbContext = CreateAgendaContext(SqlConnectionString);
    var contact = CreateTestContact();

    AddContact(dbContext, contact);
    await SaveChangesAsync(dbContext);
    ClearChangeTracker(dbContext);

    var actual = await FindContactByEmail(dbContext.Contacts.AsQueryable(), contact.ContactEmail).SingleAsync();
    Assert.IsNotNull(actual);
  }

  [TestMethod]
  public void contacts__find_contacts_page__paged_contacts()
  {
    using var dbContext = CreateAgendaContext(SqlConnectionString);
    Contact[] contacts = [CreateTestContact(), CreateTestContact(), CreateTestContact(), CreateTestContact(), CreateTestContact()];

    var actual = FindContactsPage(contacts.AsQueryable(), 1, 2);
    AreEqual(actual, [contacts[2], contacts[3]]);
  }
}