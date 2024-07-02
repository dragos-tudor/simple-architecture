
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static bool ExistsMessage (Message? message) => message is not null;

  public static bool ExistMessages (IEnumerable<Message> messages) => messages.Any();

  public static bool HasMessageContent (Message message) => message.MessageContent != null;
}