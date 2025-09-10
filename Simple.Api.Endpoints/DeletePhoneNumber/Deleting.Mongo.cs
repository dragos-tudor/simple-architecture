
using MongoDB.Driver;

namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  public static async Task<Results<Ok, ProblemHttpResult>> DeletePhoneNumberMongoAsync(
    Guid contactId,
    short countryCode,
    long number,
    IMongoDatabase mongoDatabase,
    CancellationToken cancellationToken = default)
  {
    var contactColl = GetContactCollection(mongoDatabase);

    var (_, error) = await DeletePhoneNumberService(
      contactId,
      CreatePhoneNumber(countryCode, number),
      (contactId, cancellationToken) => FindContactById(contactColl.AsQueryable(), contactId).FirstOrDefaultAsync(cancellationToken) as Task<Contact?>,
      (contact, phoneNumber, cancellationToken) => DeletePhoneNumberAsync(contactColl, contact, phoneNumber, cancellationToken),
      cancellationToken);

    return error is null ?
      TypedResults.Ok() :
      TypedResults.Problem(error);
  }
}