
using MongoDB.Driver;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  static async Task FinalizeMongoMessage (Message message, IMongoDatabase agendaDb, CancellationToken cancellationToken = default)
  {
    var messages = GetMessageCollection(agendaDb);
    await UpdateMessageIsActive(messages, message, false, cancellationToken);
  }
}