
namespace Simple.Messaging.Handlers;

partial class HandlersFuncs
{
  public static async Task<Message> FinalizeMessageSqlAsync(AgendaContext dbContext, Message message, CancellationToken cancellationToken = default)
  {
    UpdateMessage(dbContext, message, message => SetMessageIsPending(message, false));
    await SaveChangesAsync(dbContext, cancellationToken);
    return message;
  }
}