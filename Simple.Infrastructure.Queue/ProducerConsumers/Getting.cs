
namespace Simple.Infrastructure.Queue;

partial class QueueFuncs
{
  public static string? GetMessageType<TMessage> (TMessage? message) => message is Message? (message as Message)?.MessageType: default;

  static string? GetMessageTraceId<TMessage> (TMessage? message) => message is Message? (message as Message)?.TraceId: default;
}