using Microsoft.Extensions.Logging;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  static readonly ILogger Logger = CreateLogger(GetLoggerFactory(), typeof(NotificationsFuncs).Namespace!);
}