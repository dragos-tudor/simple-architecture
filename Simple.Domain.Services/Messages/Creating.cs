
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static Message<TPayload> CreateMessage<TPayload> (
    TPayload messagePayload,
    Guid? messageId = default,
    string? traceId = default)
  =>
    new () {
      MessageId = messageId ?? Guid.Empty,
      MessageContent = Empty,
      MessageType = GetTypeName(typeof(TPayload)),
      MessagePayload = messagePayload,
      TraceId = traceId
    };
}