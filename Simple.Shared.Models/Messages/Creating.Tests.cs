
namespace Simple.Shared.Models;

partial class ModelsTests
{
  public static Message CreateTestMessage (
    Guid? messageId = default,
    string? messageType = default,
    string? messageContent = default,
    DateTime? messageData = default,
    Guid? parentId = default,
    string? traceId = default,
    bool? isActive = default)
  =>
    new () {
      MessageId = messageId ?? GetRandomGuid(),
      MessageType = messageType ?? GetRandomString(MessageContraints.MessageTypeMaxLength),
      MessageContent = messageContent ?? GetRandomString(50),
      MessageDate = messageData ?? GetRandomDate(),
      ParentId = parentId ?? GetRandomGuid(),
      TraceId = traceId ?? GetRandomString(MessageContraints.TraceIdMaxLength),
      IsActive = isActive ?? GetRandomInt(0, 1) == 1? true: false
    };
}
