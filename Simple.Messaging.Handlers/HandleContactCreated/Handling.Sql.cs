
using Microsoft.EntityFrameworkCore;


namespace Simple.Messaging.Handlers;

partial class HandlersFuncs
{
  public static async Task<string?> HandleContactCreatedSqlAsync(
    Message<ContactCreatedEvent> message,
    AgendaContextFactory dbContextFactory,
    MailServerOptions mailServerOptions,
    DateTime currentDate,
    CancellationToken cancellationToken = default)
  {
    using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
    var messages = dbContext.Messages;

    await HandleContactCreatedAsync(
      message,
      "dragos.tudor@gmail.com",
      currentDate,
      (messageIdempotency, cancellationToken) => FindMessageDuplication(messages.AsQueryable(), messageIdempotency).FirstOrDefaultAsync(cancellationToken),
      (@event, cancellationToken) => SendMailMessageAsync(ToMimeMessage(@event), mailServerOptions, cancellationToken),
      (message, cancellationToken) => InsertMessageAsync(dbContext, message, cancellationToken),
      cancellationToken
    );

    return default;
  }
}
