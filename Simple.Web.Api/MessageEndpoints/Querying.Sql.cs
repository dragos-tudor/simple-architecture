
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static async Task<Ok<List<Message>>> GetMessagesPageSqlEndpoint (short? pageIndex, short? pageSize, AgendaContextFactory agendaContextFactory, HttpContext httpContext)
  {
    using var agendaContext = await agendaContextFactory.CreateDbContextAsync();
    return TypedResults.Ok(await GetMessagesPage(agendaContext.Messages.AsQueryable(), pageSize, pageIndex).ToListAsync(httpContext.RequestAborted));
  }
}