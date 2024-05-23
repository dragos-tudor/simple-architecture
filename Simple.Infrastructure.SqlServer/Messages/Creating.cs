
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Message CreateMessage<T> (
    T messagePayload,
    Guid? messageId = default,
    DateTime? messageDate = default,
    Guid? parentId = default,
    string? traceId = default) where T: class
  =>
    new () {
      MessageId = messageId ?? Guid.Empty,
      MessageType = typeof(T).Name,
      MessageDate = messageDate ?? DateTime.UtcNow,
      MessageContent = SerializeObject(messagePayload),
      MessageVersion = 1,
      ParentId = parentId,
      TraceId = traceId,
      IsActive = true
    };
}