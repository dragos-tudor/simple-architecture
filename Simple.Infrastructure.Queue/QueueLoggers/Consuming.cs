
using Microsoft.Extensions.Logging;

namespace Simple.Infrastructure.Queue;

partial class QueueFuncs
{
  [LoggerMessage(1, LogLevel.Error, "Consumer: consuming messages error {Error}.", EventName = "ConsumerError")]
  public static partial void LogConsumerError (ILogger logger, string error);

  [LoggerMessage(2, LogLevel.Error, "Consumer: consuming messages canceled.", EventName = "ConsumerCanceledError")]
  public static partial void LogConsumerCanceledError (ILogger logger);

  [LoggerMessage(3, LogLevel.Information, "Consumer: dequeued message {Message}.", EventName = "LogConsumerDequeuedMessage")]
  public static partial void LogConsumerDequeuedMessage (ILogger logger, string? message);
}