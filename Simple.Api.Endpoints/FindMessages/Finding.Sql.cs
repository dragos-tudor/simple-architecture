
using Microsoft.EntityFrameworkCore;

namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  public static async Task<Ok<List<Message>>> FindMessagesSqlAsync(short? pageIndex, short? pageSize, AgendaContextFactory sqlContextFactory, CancellationToken cancellationToken = default)
  {
    using var dbContext = await sqlContextFactory.CreateDbContextAsync();
    var messageQuery = dbContext.Messages.AsQueryable();
    var messages = await FindMessagesPage(messageQuery, pageSize, pageIndex).ToListAsync(cancellationToken);

    return TypedResults.Ok(messages);
  }
}