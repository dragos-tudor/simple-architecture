
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static Message<T> CreateMessage<T> (
    T messagePayload,
    Guid? messageId = default,
    DateTime? messageDate = default,
    Guid? parentId = default,
    string? traceId = default,
    bool isActive = true)
  =>
    new () {
      MessageId = messageId ?? Guid.Empty,
      MessageType = typeof(T).Name,
      MessageDate = messageDate ?? DateTime.UtcNow,
      MessagePayload = messagePayload,
      MessageContent = SerializePayload(messagePayload),
      MessageVersion = 1,
      ParentId = parentId,
      TraceId = traceId,
      IsActive = isActive
    };

  public static Message<T> CreateFromMessage<T> (
    T messagePayload,
    Message message,
    Guid? messageId = default,
    DateTime? messageDate = default,
    bool isActive = true)
  =>
    new () {
      MessageId = messageId ?? Guid.Empty,
      MessageType = typeof(T).Name,
      MessageDate = messageDate ?? DateTime.UtcNow,
      MessagePayload = messagePayload,
      MessageContent = SerializePayload(messagePayload),
      MessageVersion = 1,
      ParentId = message.MessageId,
      TraceId = message.TraceId,
      IsActive = isActive
    };
}