
using MongoDB.Driver;

namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  public static async Task<Results<Ok<Contact>, NotFound>> FindContactMongoAsync(Guid contactId, IMongoDatabase mongoDatabase, CancellationToken cancellationToken = default)
  {
    var contactColl = GetContactCollection(mongoDatabase);
    var contact = await FindContactById(contactColl.AsQueryable(), contactId).FirstOrDefaultAsync(cancellationToken);

    return contact is not null ?
      TypedResults.Ok(contact) :
      TypedResults.NotFound();
  }
}