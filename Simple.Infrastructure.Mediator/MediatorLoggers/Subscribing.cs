
using Microsoft.Extensions.Logging;

namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  [LoggerMessage(12, LogLevel.Information, "Subscriber: subscriber subscribed {SubscriberId}.", EventName = "LogSubscribedSubscriber")]
  public static partial void LogSubscribedSubscriber (ILogger logger, string subscriberId);

  [LoggerMessage(14, LogLevel.Error, "Subscriber: subscribing subscriber {SubscriberId} error {Error}.", EventName = "LogSubscribingSubscriberError")]
  public static partial void LogSubscribingSubscriberError (ILogger logger, string subscriberId, string error);
}