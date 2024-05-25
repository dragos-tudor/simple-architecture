
using System.Collections.Concurrent;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  static readonly ConcurrentBag<MimeMessage> MailMessages = [];

  internal static void StoreMailMessage (MimeMessage mailMessage, ConcurrentBag<MimeMessage>? mailMessages = default) =>
    (mailMessages ?? MailMessages).Add(mailMessage);
}