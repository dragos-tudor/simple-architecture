
using System.Collections.Generic;
using MongoDB.Driver;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  static async Task<Ok<List<Message>>> GetMessagesMongoEndpoint (short? pageIndex, short? pageSize, IMongoDatabase agendaDb, HttpContext httpContext) =>
    TypedResults.Ok(await MongoDbFuncs.FindMessages(GetMessageCollection(agendaDb).AsQueryable(), pageSize, pageIndex).ToListAsync(httpContext.RequestAborted));
}