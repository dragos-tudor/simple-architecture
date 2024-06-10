
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  static async Task<Ok<Contact>> GetContactSqlEndpoint (Guid contactId, AgendaContextFactory agendaContextFactory, HttpContext httpContext)
  {
    using var agendaContext = await agendaContextFactory.CreateDbContextAsync();
    return TypedResults.Ok(await FindContactByKey(agendaContext.Contacts, contactId).Include(c => c.PhoneNumbers).FirstOrDefaultAsync(httpContext.RequestAborted));
  }

  static async Task<Ok<List<Contact>>> GetContactsSqlEndpoint (short? pageIndex, short? pageSize, AgendaContextFactory agendaContextFactory, HttpContext httpContext)
  {
    using var agendaContext = await agendaContextFactory.CreateDbContextAsync();
    return TypedResults.Ok(await FindContacts(agendaContext.Contacts, pageSize, pageIndex).Include(c => c.PhoneNumbers).ToListAsync(httpContext.RequestAborted));
  }
}