
using System.Reflection;

namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  static Message<T> RestoreMessage<T> (Message message, T messagePayload) =>
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

  public static Message RestoreMessage (Message message)
  {
    var bindingFlags = BindingFlags.NonPublic | BindingFlags.Static;
    var restoreMessage = typeof(ModelsFuncs).GetMethod(nameof(RestoreMessage), bindingFlags)!;

    var messagePayloadType = GetMessagePayloadType(message);
    var genericRestoreMessage = restoreMessage.MakeGenericMethod(messagePayloadType);

    var messagePayload = DeserializeMessagePayload(message);
    return (Message)genericRestoreMessage.Invoke(null, [message, messagePayload])!;
  }
}