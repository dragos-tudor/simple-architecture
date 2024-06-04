
namespace Simple.Domain.Services;

partial class ServicesFuncs
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
      MessageContent = SerializeObject(messagePayload),
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
      MessageContent = SerializeObject(messagePayload),
      MessageVersion = 1,
      ParentId = message.MessageId,
      TraceId = message.TraceId,
      IsActive = isActive
    };
}