
using SmtpServer;
using SmtpServer.Protocol;
using SmtpServer.Storage;

namespace Simple.Infrastructure.Notifications;

public sealed class SmtpServerStore : MessageStore
{
  public override async Task<SmtpResponse> SaveAsync(ISessionContext context, IMessageTransaction transaction, ReadOnlySequence<byte> buffer, CancellationToken cancellationToken)
  {
    var mailMessage = await ReadMailMessage(buffer, cancellationToken);
    StoreMailMessage(mailMessage);
    return SmtpResponse.Ok;
  }
}
