
using System.Collections.Generic;
using MongoDB.Driver;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  static async Task<Ok<List<Message>>> GetMessagesPageMongoEndpoint (short? pageIndex, short? pageSize, IMongoDatabase agendaDb, HttpContext httpContext) =>
    TypedResults.Ok(await MongoDbFuncs.GetMessagesPage(GetMessageCollection(agendaDb).AsQueryable(), pageSize, pageIndex).ToListAsync(httpContext.RequestAborted));
}