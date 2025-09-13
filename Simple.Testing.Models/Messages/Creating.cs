
using System.Text.Json;

namespace Simple.Testing.Models;

partial class ModelsFuncs
{
  public static Message CreateTestMessage(
    Guid? messageId = default,
    string? messageType = default,
    string? messageContent = default,
    DateTime? messageDate = default,
    Guid? parentId = default,
    string? correlationId = default,
    bool? isPending = default)
  =>
    new()
    {
      MessageId = messageId ?? GetRandomGuid(),
      MessageType = messageType ?? GetRandomString(MessageContraints.MessageTypeMaxLength),
      MessageContent = messageContent ?? GetRandomString(50),
      MessageDate = messageDate ?? GetRandomDate(),
      ParentId = parentId ?? GetRandomGuid(),
      CorrelationId = correlationId ?? GetRandomString(MessageContraints.CorrelationIdMaxLength),
      IsPending = isPending ?? (GetRandomInt(0, 1) == 1)
    };

  public static Message<TPayload> CreateTestMessage<TPayload>(
    TPayload messagePayload,
    Guid? messageId = default,
    DateTime? messageDate = default,
    Guid? parentId = default,
    string? correlationId = default,
    bool? isPending = default)
  =>
    new()
    {
      MessageId = messageId ?? GetRandomGuid(),
      MessageType = messagePayload!.GetType().FullName!,
      MessageContent = messagePayload.GetType().IsClass ? JsonSerializer.Serialize(messagePayload) : messagePayload.ToString()!,
      MessagePayload = messagePayload,
      MessageDate = messageDate ?? GetRandomDate(),
      ParentId = parentId ?? GetRandomGuid(),
      CorrelationId = correlationId ?? GetRandomString(MessageContraints.CorrelationIdMaxLength),
      IsPending = isPending ?? (GetRandomInt(0, 1) == 1)
    };
}
