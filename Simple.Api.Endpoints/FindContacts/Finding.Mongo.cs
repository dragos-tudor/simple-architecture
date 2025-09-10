
using MongoDB.Driver;

namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  public static async Task<Ok<List<Contact>>> FindContactsMongoAsync(short? pageIndex, short? pageSize, IMongoDatabase mongoDatabase, CancellationToken cancellationToken = default)
  {
    var contactColl = GetContactCollection(mongoDatabase).AsQueryable();
    var contacts = await FindContactsPage(contactColl, pageIndex, pageSize).ToListAsync(CancellationToken.None);

    return TypedResults.Ok(contacts);
  }
}