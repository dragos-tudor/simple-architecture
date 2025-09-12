
namespace Simple.Messaging.Handlers;

partial class HandlersFuncs
{
  public static async Task<Message> FinalizeMessageSqlAsync(Message message, AgendaContext dbContext, CancellationToken cancellationToken = default)
  {
    SetMessageIsPending(message, false);
    UpdateMessage(dbContext, message);

    await SaveChangesAsync(dbContext, cancellationToken);
    return message;
  }
}