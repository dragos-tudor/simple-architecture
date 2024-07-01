
using System.Reflection;

namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  static DateTime GetMessageDateDelay (DateTime date, TimeSpan dateDelay) => date - dateDelay;

  public static byte GetMessageFailureCounter (Message message) => (byte)(message.FailureCounter is null? 0: message.FailureCounter);
}