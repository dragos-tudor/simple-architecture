
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static byte GetMessageFailureCounter (Message message) => (byte)(message.FailureCounter is null? 0: message.FailureCounter);
}