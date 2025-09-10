
namespace Simple.Infrastructure.JobScheduler;

partial class JobSchedulerFuncs
{
  static Guid GetMessageId<TMessage>(TMessage message) where TMessage : Message => message.MessageId;

  static string? GetMessageCorrelationId<TMessage>(TMessage message) where TMessage : Message => message.CorrelationId;

  static string GetMessageType<TMessage>(TMessage message) where TMessage : Message => message.MessageType;

  static DateTime GetMinMessageDate(DateTime currentDate, MessageJobOptions jobOptions) => currentDate - jobOptions.MinTime;

  static DateTime GetMaxMesageDate(DateTime currentDate, MessageJobOptions jobOptions) => currentDate - jobOptions.MaxTime;

  static DateTime GetMaxMessageDate<TMessage>(IEnumerable<TMessage> messages) where TMessage : Message => messages.Max(message => message.MessageDate);
}