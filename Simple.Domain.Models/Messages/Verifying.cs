
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static bool ExistMessage (Message? message) => message is not null;

  public static bool HasMessageContent (Message message) => message.MessageContent != null;
}