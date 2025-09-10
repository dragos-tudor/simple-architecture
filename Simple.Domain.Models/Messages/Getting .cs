
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static byte GetMessageErrorCounter(Message message) => message.ErrorCounter ?? 0;
}