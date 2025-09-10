
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static Message<T> RestoreMessage<T>(Message message, T messagePayload) =>
    new()
    {
      MessageId = message.MessageId,
      MessageType = message.MessageType,
      MessageDate = message.MessageDate,
      MessagePayload = messagePayload,
      MessageContent = message.MessageContent,
      MessageVersion = message.MessageVersion,
      ParentId = message.ParentId,
      CorrelationId = message.CorrelationId,

      ErrorCounter = message.ErrorCounter,
      ErrorMessage = message.ErrorMessage,
      IsPending = message.IsPending
    };

  public static partial Message RestoreMessage(Message message);
}