
namespace Simple.App.Services;

partial class ServicesFuncs
{
  static IEnumerable<Message> EnqueueMessages (Channel<Message> queue, IEnumerable<Message> messages)
  {
    foreach(var message in messages.Where(HasMessageContent))
      EnqueueMessage(queue, CreateFromMessage(DeserializeMessagePayload(message), message));
    return messages;
  }
}