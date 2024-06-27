
using MongoDB.Driver;

namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  public static async Task<Ok<List<Message>>> FindMessagesPageMongoEndpoint (short? pageIndex, short? pageSize, IMongoDatabase agendaDb, HttpContext httpContext) =>
    TypedResults.Ok(await FindMessagesPage(GetMessageCollection(agendaDb).AsQueryable(), pageSize, pageIndex).ToListAsync(httpContext.RequestAborted));
}