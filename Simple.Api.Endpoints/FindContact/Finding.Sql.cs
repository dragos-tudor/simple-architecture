
using Microsoft.EntityFrameworkCore;

namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  public static async Task<Results<Ok<Contact>, NotFound>> FindContactSqlAsync(Guid contactId, AgendaContextFactory dbContextFactory, CancellationToken cancellationToken = default)
  {
    using var dbContext = CreateAgendaContext(dbContextFactory);
    var contactQuery = dbContext.Contacts.AsQueryable();
    var contact = await FindContactById(contactQuery, contactId).Include(c => c.PhoneNumbers).FirstOrDefaultAsync(cancellationToken);

    return contact is not null ?
      TypedResults.Ok(contact) :
      TypedResults.NotFound();
  }
}