
namespace Simple.Infrastructure.Queue;

partial class QueueFuncs
{
  public static Guid? GetMessageId<TMessage> (TMessage? message) => message is Message? (message as Message)?.MessageId: default;

  public static string? GetMessageType<TMessage> (TMessage? message) => message is Message? (message as Message)?.MessageType: default;

  static string? GetMessageCorrelationId<TMessage> (TMessage? message) => message is Message? (message as Message)?.CorrelationId: default;
}