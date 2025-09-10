
using MongoDB.Driver;

namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  public static async Task<Ok<List<Message>>> FindMessagesMongoAsync(short? pageIndex, short? pageSize, IMongoDatabase mongoDatabase, CancellationToken cancellationToken = default)
  {
    var messageColl = GetMessageCollection(mongoDatabase).AsQueryable();
    var messages = await FindMessagesPage(messageColl, pageSize, pageIndex).ToListAsync(CancellationToken.None);

    return TypedResults.Ok(messages);
  }
}