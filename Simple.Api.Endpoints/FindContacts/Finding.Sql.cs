
using Microsoft.EntityFrameworkCore;

namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  public static async Task<Ok<List<Contact>>> FindContactsSqlAsync(short? pageIndex, short? pageSize, string sqlConnectionString, CancellationToken cancellationToken = default)
  {
    using var dbContext = CreateAgendaContext(sqlConnectionString);
    var contactQuery = dbContext.Contacts.AsQueryable();
    var contacts = await FindContactsPage(contactQuery, pageIndex, pageSize).Include(c => c.PhoneNumbers).ToListAsync(cancellationToken);

    return TypedResults.Ok(contacts);
  }
}