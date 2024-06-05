
using SmtpServer;
using SmtpServer.Protocol;
using SmtpServer.Storage;

namespace Simple.Infrastructure.Notifications;

public sealed class SmtpServerStore<TNotification> (IProducerConsumerCollection<TNotification> Notifications, Func<MimeMessage, TNotification> mapMessage) : MessageStore
{
  public override async Task<SmtpResponse> SaveAsync (
    ISessionContext context,
    IMessageTransaction transaction,
    ReadOnlySequence<byte> buffer,
    CancellationToken cancellationToken)
  {
    var mailMessage = await ReadMailMessage(buffer, cancellationToken);
    var notification = mapMessage(mailMessage);

    AddNotification(Notifications, notification);
    return SmtpResponse.Ok;
  }
}
