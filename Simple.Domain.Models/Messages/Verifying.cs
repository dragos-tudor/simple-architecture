
using System.Reflection;

namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static bool ExistMessage (Message? message) => message is not null;

  public static bool ExistMessages (IEnumerable<Message> messages) => messages.Any();

  public static bool HasMessageContent (Message message) => message.MessageContent != null;

  static bool IsMessageTypeAssembly (Assembly assembly, Message message) => message.MessageType.StartsWith(assembly.GetName().Name!, StringComparison.InvariantCulture);
}