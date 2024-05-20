
using Microsoft.Extensions.Logging;

namespace Simple.Infrastructure.Queue;

partial class QueueFuncs
{
  [LoggerMessage(1, LogLevel.Error, "Consumer: consuming messages error {Message}.", EventName = "ConsumerError")]
  public static partial void LogConsumerError (ILogger logger, string message);

  [LoggerMessage(2, LogLevel.Error, "Consumer: consuming messages canceled.", EventName = "ConsumerCanceledError")]
  public static partial void LogConsumerCanceledError (ILogger logger);
}