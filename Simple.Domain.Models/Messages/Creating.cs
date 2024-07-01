
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static Message<T> CreateMessage<T> (
    T messagePayload,
    Guid? messageId = default,
    DateTime? messageDate = default,
    Guid? parentId = default,
    string? correlationId = default,
    bool isActive = true)
  =>
    new () {
      MessageId = messageId ?? Guid.Empty,
      MessageType = typeof(T).Name!,
      MessageDate = messageDate ?? DateTime.UtcNow,
      MessagePayload = messagePayload,
      MessageContent = SerializePayload(messagePayload),
      MessageVersion = 1,
      ParentId = parentId,
      CorrelationId = correlationId,
      IsActive = isActive
    };

  public static Message<T> CreateChildMessage<T> (Message message, T messagePayload) =>
    CreateMessage(messagePayload, parentId: message.MessageId, correlationId: message.CorrelationId, isActive: false);
}