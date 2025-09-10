
namespace Simple.Infrastructure.MessageQueue;

partial class MessageQueueFuncs
{
  static string? GetMessageCorrelationId<TMessage>(TMessage message) where TMessage : Message => message.CorrelationId;

  static Guid GetMessageId<TMessage>(TMessage message) where TMessage : Message => message.MessageId;

  static string GetMessageType<TMessage>(TMessage message) where TMessage : Message => message.MessageType;
}