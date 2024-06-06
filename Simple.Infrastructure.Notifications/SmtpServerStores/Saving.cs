
using SmtpServer;
using SmtpServer.Protocol;
using SmtpServer.Storage;

namespace Simple.Infrastructure.Notifications;

public sealed class SmtpServerStore<TNotification> (Action<TNotification> handleNotification, Func<MimeMessage, TNotification> mapMessage) : MessageStore
{
  public override async Task<SmtpResponse> SaveAsync (
    ISessionContext context,
    IMessageTransaction transaction,
    ReadOnlySequence<byte> buffer,
    CancellationToken cancellationToken)
  {
    var mailMessage = await ReadMailMessage(buffer, cancellationToken);
    var notification = mapMessage(mailMessage);

    handleNotification(notification);
    return SmtpResponse.Ok;
  }
}
