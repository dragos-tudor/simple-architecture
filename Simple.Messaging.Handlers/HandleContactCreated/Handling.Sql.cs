
using Microsoft.EntityFrameworkCore;


namespace Simple.Messaging.Handlers;

partial class HandlersFuncs
{
  public static Task<NotificationSentEvent?> HandleContactCreatedSqlAsync(
    Message<ContactCreatedEvent> message,
    AgendaContext dbContext,
    MailServerOptions mailServerOptions,
    DateTime currentDate,
    CancellationToken cancellationToken = default)
  {
    var messages = dbContext.Messages;

    return HandleContactCreatedAsync(
      message,
      "dragos.tudor@gmail.com",
      currentDate,
      (messageIdempotency, cancellationToken) => FindMessageDuplication(messages.AsQueryable(), messageIdempotency).FirstOrDefaultAsync(cancellationToken),
      (@event, cancellationToken) => SendMailMessageAsync(ToMimeMessage(@event), mailServerOptions, cancellationToken),
      (message, cancellationToken) => InsertMessageAsync(dbContext, message, cancellationToken),
      cancellationToken
    );
  }
}
