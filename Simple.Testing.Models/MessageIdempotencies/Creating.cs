
namespace Simple.Testing.Models;

partial class ModelsFuncs
{
  public static MessageIdempotency CreateTestMessageIdempotency(
    Guid? parentId = default,
    string? messageType = default)
  =>
    new(
      parentId ?? GetRandomGuid(),
      messageType ?? GetRandomString(MessageContraints.MessageTypeMaxLength)
    );
}
