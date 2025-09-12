#pragma warning disable IDE0059

using Microsoft.Extensions.Logging;

namespace Simple.Infrastructure.JobScheduler;

partial class JobSchedulerFuncs
{
  internal static async Task<Exception?> ProcessMessageAsync<TMessage>(
    TMessage message,
    ProcessMessage<TMessage> processMessage,
    HandleMessageError<TMessage> handleMessageError,
    ILogger logger,
    CancellationToken cancellationToken) where TMessage : Message
  {
    var messageId = GetMessageId(message);
    var messageType = GetMessageType(message);
    var correlationId = GetMessageCorrelationId(message);

    try
    {
      LogProcessMessage(logger, messageId, messageType, correlationId);
      await processMessage(message, cancellationToken);
      return default;
    }
    catch (OperationCanceledException exception) { return exception; }
    catch (Exception exception)
    {
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

  public static async Task ProcessMessagesAsync(
    QueryMessages<Message> queryMessages,
    ProcessMessage<Message> processMessage,
    HandleMessageError<Message> handleMessageError,
    MessageJobOptions jobOptions,
    TimeProvider timeProvider,
    ILogger logger,
    CancellationToken cancellationToken = default)
  {
    var currentDate = timeProvider.GetUtcNow().UtcDateTime;
    var exclusiveMinDate = GetMinMessageDate(currentDate, jobOptions);
    var inclusiveMaxDate = GetMaxMesageDate(currentDate, jobOptions);
    var batchSize = jobOptions.BatchSize;
    var lastMessageDate = exclusiveMinDate;

    LogStartProcessMessages(logger, exclusiveMinDate, inclusiveMaxDate);

    while (true)
    {
      IEnumerable<Message> messages = [];
      try
      {
        messages = await queryMessages(lastMessageDate, inclusiveMaxDate, batchSize, cancellationToken);
      }
      catch (OperationCanceledException) { break; }
      catch (Exception exception)
      {
        LogQueryMessagesError(logger, exception.Message, exception.StackTrace);
        break;
      }

      if (!ExistMessages(messages)) break;
      foreach (var message in messages)
      {
        var result = await ProcessMessageAsync(message, processMessage, handleMessageError, logger, cancellationToken);
        if (result is OperationCanceledException) break;
      }

      lastMessageDate = GetMaxMessageDate(messages);
    }

    LogEndProcessMessages(logger, exclusiveMinDate, inclusiveMaxDate);
  }


}