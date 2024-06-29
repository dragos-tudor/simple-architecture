
using Microsoft.EntityFrameworkCore;

namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  public static async Task<Ok<Contact>> FindContactSqlEndpoint (Guid contactId, AgendaContextFactory sqlContextFactory, HttpContext httpContext)
  {
    using var agendaContext = await sqlContextFactory.CreateDbContextAsync();
    return TypedResults.Ok(await FindContactByKey(agendaContext.Contacts.AsQueryable(), contactId).Include(c => c.PhoneNumbers).FirstOrDefaultAsync(httpContext.RequestAborted));
  }

  public static async Task<Ok<List<Contact>>> FindContactsPageSqlEndpoint (short? pageIndex, short? pageSize, AgendaContextFactory sqlContextFactory, HttpContext httpContext)
  {
    using var agendaContext = await sqlContextFactory.CreateDbContextAsync();
    return TypedResults.Ok(await FindContactsPage(agendaContext.Contacts.AsQueryable(), pageIndex, pageSize).Include(c => c.PhoneNumbers).ToListAsync(httpContext.RequestAborted));
  }
}