
namespace Simple.Messaging.Handlers;

partial class HandlersFuncs
{
  public static async Task<Message> HandleMessageErrorSqlAsync(AgendaContext dbContext, Message message, Exception exception, byte maxErrors, CancellationToken cancellationToken = default)
  {
    var errorCounter = GetMessageErrorCounter(message) + 1;

    SetMessageErrorMessage(message, exception.ToString());
    SetMessageErrorCounter(message, (byte)errorCounter);
    SetMessageIsPending(message, ShouldRetryProcessMessage(message, maxErrors));

    UpdateMessage(dbContext, message);
    await SaveChangesAsync(dbContext, cancellationToken);
    return message;
  }
}