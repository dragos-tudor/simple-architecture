
using Microsoft.EntityFrameworkCore;

namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  public static async Task<string?> NotifyAddedToAgendaSqlAsync(
    Message<ContactCreatedEvent> message,
    AgendaContextFactory dbContextFactory,
    string mailServerName,
    int smtpPort,
    DateTime currentDate,
    CancellationToken cancellationToken = default)
  {
    using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);
    var messages = dbContext.Messages;

    await NotifyAddedToAgendaAsync(
      message,
      "dragos.tudor@gmail.com",
      currentDate,
      (messageIdempotency, cancellationToken) => FindMessageDuplication(messages.AsQueryable(), messageIdempotency).FirstOrDefaultAsync(cancellationToken),
      (notification, cancellationToken) => SendNotificationAsync(notification, mailServerName, smtpPort, cancellationToken),
      (message, cancellationToken) => InsertMessageAsync(dbContext, message, cancellationToken),
      cancellationToken
    );

    return default;
  }
}
