
namespace Simple.Api.Endpoints;

partial class EndpointsTests
{
  [TestMethod]
  public async Task contact__find_sql_contact__contact_found()
  {
    using var dbContext = CreateAgendaContext(SqlConnectionString);
    var contact = CreateTestContact();

    await InsertContactSqlAsync(dbContext, contact);
    ClearChangeTracker(dbContext);

    var result = await FindContactSqlAsync(contact.ContactId, SqlConnectionString, CancellationToken.None);

    var httpContext = await ExecuteHttpResultAsync(result, CreateHttpContext());
    Assert.AreEqual(contact, await ReadHttpResponseJsonAsync<Contact>(httpContext.Response));
    Assert.AreEqual(200, httpContext.Response.StatusCode);
  }
}