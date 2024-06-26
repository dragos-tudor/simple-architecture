
namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  static IEnumerable<Message> EnqueueMessages (Channel<Message> queue, IEnumerable<Message> messages)
  {
    foreach(var message in messages.Where(HasMessageContent))
      EnqueueMessage(queue, RestoreMessage(message, DeserializeMessagePayload(message)));
    return messages;
  }
}