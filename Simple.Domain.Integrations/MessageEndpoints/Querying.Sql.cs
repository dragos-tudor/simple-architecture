
using Microsoft.EntityFrameworkCore;

namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  public static async Task<Ok<List<Message>>> FindMessagesPageSqlEndpoint (short? pageIndex, short? pageSize, AgendaContextFactory sqlContextFactory, HttpContext httpContext)
  {
    using var agendaContext = await sqlContextFactory.CreateDbContextAsync();
    return TypedResults.Ok(await FindMessagesPage(agendaContext.Messages.AsQueryable(), pageSize, pageIndex).ToListAsync(httpContext.RequestAborted));
  }
}