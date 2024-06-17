
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static MessageIdempotency CreateMessageIdempotency<TPayload> (Message parentMessage) => new (parentMessage.MessageId, typeof(TPayload).Name);

  public static MessageIdempotency CreateMessageIdempotency(Message parentMessage, string messageType) => new (parentMessage.MessageId, messageType);
}