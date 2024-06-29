
using MongoDB.Driver;

namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  public static async Task<Ok<Contact>> FindContactMongoEndpoint (Guid contactId, IMongoDatabase mongoDatabase, HttpContext httpContext) =>
    TypedResults.Ok(await FindContactByKey(GetContactCollection(mongoDatabase).AsQueryable(), contactId).FirstOrDefaultAsync(httpContext.RequestAborted));

  public static async Task<Ok<List<Contact>>> FindContactsPageMongoEndpoint (short? pageIndex, short? pageSize, IMongoDatabase mongoDatabase, HttpContext httpContext) =>
    TypedResults.Ok(await FindContactsPage(GetContactCollection(mongoDatabase).AsQueryable(), pageIndex, pageSize).ToListAsync(httpContext.RequestAborted));
}