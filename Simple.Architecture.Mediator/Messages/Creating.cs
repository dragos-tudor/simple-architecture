
namespace Simple.Architecture.Mediator;

partial class MediatorFuncs
{
  public static Message<T> CreateMessage<T> (
    T messagePayload,
    Guid? messageId = default,
    string? traceId = default)
  =>
    new () {
      MessageId = messageId ?? Guid.Empty,
      MessageType = typeof(T).Name,
      MessagePayload = messagePayload,
      TraceId = traceId
    };
}