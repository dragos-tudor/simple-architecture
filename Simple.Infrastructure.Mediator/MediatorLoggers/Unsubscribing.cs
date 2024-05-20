
using Microsoft.Extensions.Logging;

namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  [LoggerMessage(13, LogLevel.Information, "Unsubscriber: subscriber unsubscribed {SubscriberId}.", EventName = "LogUnsubscribedSubscriber")]
  public static partial void LogUnsubscribedSubscriber (ILogger logger, string subscriberId);
}