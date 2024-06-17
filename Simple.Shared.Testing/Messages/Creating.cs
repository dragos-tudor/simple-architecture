
namespace Simple.Shared.Testing;

partial class TestingFuncs
{
  public static Message CreateTestMessage (
    Guid? messageId = default,
    string? messageType = default,
    string? messageContent = default,
    DateTime? messageDate = default,
    Guid? parentId = default,
    string? correlationId = default,
    bool? isActive = default)
  =>
    new () {
      MessageId = messageId ?? GetRandomGuid(),
      MessageType = messageType ?? GetRandomString(MessageContraints.MessageTypeMaxLength),
      MessageContent = messageContent ?? GetRandomString(50),
      MessageDate = messageDate ?? GetRandomDate(),
      ParentId = parentId ?? GetRandomGuid(),
      CorrelationId = correlationId ?? GetRandomString(MessageContraints.CorrelationIdMaxLength),
      IsActive = isActive ?? GetRandomInt(0, 1) == 1? true: false
    };
}
