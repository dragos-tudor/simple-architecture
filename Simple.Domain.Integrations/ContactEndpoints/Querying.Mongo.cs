
using MongoDB.Driver;

namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  public static async Task<Ok<Contact>> FindContactMongoEndpoint (Guid contactId, IMongoDatabase agendaDb, HttpContext httpContext) =>
    TypedResults.Ok(await FindContactByKey(GetContactCollection(agendaDb).AsQueryable(), contactId).FirstOrDefaultAsync(httpContext.RequestAborted));

  public static async Task<Ok<List<Contact>>> GetContactsPageMongoEndpoint (short? pageIndex, short? pageSize, IMongoDatabase agendaDb, HttpContext httpContext) =>
    TypedResults.Ok(await GetContactsPage(GetContactCollection(agendaDb).AsQueryable(), pageIndex, pageSize).ToListAsync(httpContext.RequestAborted));
}