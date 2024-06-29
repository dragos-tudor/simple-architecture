
using MongoDB.Driver;

namespace Simple.Infrastructure.Integrations;

partial class IntegrationsFuncs
{
  static async Task FinalizeMongoMessage (Message message, IMongoDatabase mongoDatabase, CancellationToken cancellationToken = default)
  {
    var messages = GetMessageCollection(mongoDatabase);
    await UpdateMessageIsActiveAsync(messages, message, false, cancellationToken);
  }
}