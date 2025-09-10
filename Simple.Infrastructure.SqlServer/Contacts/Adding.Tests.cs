
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
  [TestMethod]
  public async Task new_contact__add_contact__contact_stored()
  {
    using var dbContext = CreateAgendaContext(SqlConnectionString);
    var contact = CreateTestContact();

    AddContact(dbContext, contact);
    await SaveChangesAsync(dbContext);
    ClearChangeTracker(dbContext);

    var actual = await FindContactById(dbContext.Contacts.AsQueryable(), contact.ContactId).SingleAsync();
    Assert.AreEqual(actual, contact);
  }
}