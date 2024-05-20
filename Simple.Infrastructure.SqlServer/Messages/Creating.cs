
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static MessageDb CreateMessage<T> (
    T messagePayload,
    Guid? messageId = default,
    DateTime? messageDate = default,
    Guid? parentId = default,
    string? traceId = default)
  =>
    new () {
      MessageId = messageId ?? Guid.Empty,
      MessageType = typeof(T).Name,
      MessageDate = messageDate ?? DateTime.UtcNow,
      MessageContent = SerializeMessagePayload(messagePayload),
      MessageVersion = 1,
      ParentId = parentId,
      TraceId = traceId,
      IsActive = true
    };
}