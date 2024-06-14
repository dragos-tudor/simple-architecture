
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static async Task<Ok<Contact>> FindContactSqlEndpoint (Guid contactId, AgendaContextFactory agendaContextFactory, HttpContext httpContext)
  {
    using var agendaContext = await agendaContextFactory.CreateDbContextAsync();
    return TypedResults.Ok(await FindContactByKey(agendaContext.Contacts.AsQueryable(), contactId).Include(c => c.PhoneNumbers).FirstOrDefaultAsync(httpContext.RequestAborted));
  }

  internal static async Task<Ok<List<Contact>>> GetContactsPageSqlEndpoint (short? pageIndex, short? pageSize, AgendaContextFactory agendaContextFactory, HttpContext httpContext)
  {
    using var agendaContext = await agendaContextFactory.CreateDbContextAsync();
    return TypedResults.Ok(await GetContactsPage(agendaContext.Contacts.AsQueryable(), pageIndex, pageSize).Include(c => c.PhoneNumbers).ToListAsync(httpContext.RequestAborted));
  }
}