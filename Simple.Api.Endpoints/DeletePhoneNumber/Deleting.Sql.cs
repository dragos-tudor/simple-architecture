
using Microsoft.EntityFrameworkCore;

namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  public static async Task<Results<Ok, ProblemHttpResult>> DeletePhoneNumberSqlAsync(
    Guid contactId,
    short countryCode,
    long number,
    string sqlConnectionString,
    CancellationToken cancellationToken = default)
  {
    using var dbContext = CreateAgendaContext(sqlConnectionString);

    var (_, error) = await DeletePhoneNumberAsync(
      contactId,
      CreatePhoneNumber(countryCode, number),
      (contactId, cancellationToken) => FindContactById(dbContext.Contacts.AsQueryable(), contactId).Include(c => c.PhoneNumbers).FirstOrDefaultAsync(cancellationToken),
      (contact, phoneNumber, cancellationToken) =>
      {
        DeletePhoneNumber(dbContext, phoneNumber);
        return SaveChangesAsync(dbContext, cancellationToken);
      },
      cancellationToken);

    return error is null ?
      TypedResults.Ok() :
      TypedResults.Problem(error);
  }
}