
namespace Simple.Messaging.Handlers;

partial class HandlersFuncs
{
  public static async Task<Message> FinalizeMessageSqlAsync(AgendaContext dbContext, Message message, CancellationToken cancellationToken = default)
  {
    SetMessageIsPending(message, false);
    UpdateMessage(dbContext, message);

    await SaveChangesAsync(dbContext, cancellationToken);
    return message;
  }
}