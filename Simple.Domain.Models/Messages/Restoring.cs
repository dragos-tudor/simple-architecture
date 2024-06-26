
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static Message<T> RestoreMessage<T> (Message message, T messagePayload) =>
    new () {
      MessageId = message.MessageId,
      MessageType = messagePayload!.GetType().Name,
      MessageDate = message.MessageDate,
      MessagePayload = messagePayload,
      MessageContent = message.MessageContent,
      MessageVersion = message.MessageVersion,
      ParentId = message.ParentId,
      CorrelationId = message.CorrelationId,

      FailureCounter = message.FailureCounter,
      FailureMessage = message.FailureMessage,
      IsActive = message.IsActive
    };
}