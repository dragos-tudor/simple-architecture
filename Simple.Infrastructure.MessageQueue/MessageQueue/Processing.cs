
using System.Threading.Channels;
using Microsoft.Extensions.Logging;

namespace Simple.Infrastructure.MessageQueue;

partial class MessageQueueFuncs
{
  internal static async Task<Exception?> ProcessMessageAsync<TMessage>(
    TMessage message,
    ProcessMessage<TMessage> processMessage,
    HandleMessageError<TMessage> handleMessageError,
    ILogger logger,
    CancellationToken cancellationToken) where TMessage : Message
  {
    try
    {
      LogProcessMessage(logger, GetMessageId(message), GetMessageType(message), GetMessageCorrelationId(message));
      await processMessage(message, cancellationToken);
      return default;
    }
    catch (OperationCanceledException) { throw; }
    catch (Exception exception)
    {
      var messageId = GetMessageId(message);
      var messageType = GetMessageType(message);
      var correlationId = GetMessageCorrelationId(message);

      try
      {
        LogProcessMessageError(logger, messageId, messageType, correlationId, exception.Message, exception.StackTrace);
        await handleMessageError(message, exception, cancellationToken);
        return exception;
      }
      catch (Exception nestedException)
      {
        LogProcessMessageError(logger, messageId, messageType, correlationId, nestedException.Message, nestedException.StackTrace);
        return nestedException;
      }
    }
  }

  public static async Task<Channel<TMessage>> ProcessMessagesAsync<TMessage>(
    Channel<TMessage> queue,
    ProcessMessage<TMessage> processMessage,
    HandleMessageError<TMessage> handleMessageError,
    ILogger logger,
    CancellationToken cancellationToken = default) where TMessage : Message
  {
    LogStartProcessMessages(logger);

    while (true)
    {
      if (cancellationToken.IsCancellationRequested) break;
      try
      {
        var message = await DequeueMessage(queue, cancellationToken);
        var result = await ProcessMessageAsync(message, processMessage, handleMessageError, logger, cancellationToken);
      }
      catch (OperationCanceledException) { break; }
    }

    LogEndProcessMessages(logger);
    return queue;
  }
}