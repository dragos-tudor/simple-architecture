
namespace Simple.Domain.Integrations;

partial class IntegrationsTests
{
  [TestMethod]
  public async Task contact__find_sql_contact__found_contact ()
  {
    using var dbContext = await SqlContextFactory.CreateDbContextAsync();
    var contact = CreateTestContact();

    await InsertContactAsync(dbContext, contact);

    var actual = await FindContactSqlEndpoint(contact.ContactId, SqlContextFactory, new DefaultHttpContext());
    Assert.AreEqual(actual.Value, contact);
    Assert.AreEqual(actual.StatusCode, 200);
  }
}