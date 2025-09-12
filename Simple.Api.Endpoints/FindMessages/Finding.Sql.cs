
using Microsoft.EntityFrameworkCore;

namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  public static async Task<Ok<List<Message>>> FindMessagesSqlAsync(short? pageIndex, short? pageSize, string sqlConnectionString, CancellationToken cancellationToken = default)
  {
    using var dbContext = CreateAgendaContext(sqlConnectionString);
    var messageQuery = dbContext.Messages.AsQueryable();
    var messages = await FindMessagesPage(messageQuery, pageSize, pageIndex).ToListAsync(cancellationToken);

    return TypedResults.Ok(messages);
  }
}