
using MongoDB.Driver;

namespace Simple.App.Integrations;

partial class IntegrationsFuncs
{
  static async Task FinalizeMongoMessage (Message message, IMongoDatabase agendaDb, CancellationToken cancellationToken = default)
  {
    var messages = GetMessageCollection(agendaDb);
    await UpdateMessageIsActive(messages, message, false, cancellationToken);
  }
}