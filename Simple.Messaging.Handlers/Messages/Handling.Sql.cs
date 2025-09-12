
namespace Simple.Messaging.Handlers;

partial class HandlersFuncs
{
  public static async Task<Message> HandleMessageErrorSqlAsync(Message message, Exception exception, byte maxErrors, AgendaContext dbContext, CancellationToken cancellationToken = default)
  {
    var isPending = ShouldRetryProcessMessage(message, maxErrors);
    var errorCounter = GetMessageErrorCounter(message) + 1;

    SetMessageErrorMessage(message, exception.ToString());
    SetMessageErrorCounter(message, (byte)errorCounter);
    SetMessageIsPending(message, isPending);

    UpdateMessage(dbContext, message);
    await SaveChangesAsync(dbContext, cancellationToken);
    return message;
  }
}