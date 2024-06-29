
using System.Reflection;

namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  static DateTime GetMessageDateDelay (DateTime date, TimeSpan dateDelay) => date - dateDelay;

  static Assembly GetMessagePayloadAssembly (Message message) => AppDomain.CurrentDomain.GetAssemblies().First(assembly => IsMessageTypeAssembly(assembly, message));

  public static Type GetMessagePayloadType (Message message) => GetMessagePayloadAssembly(message).GetType(message.MessageType)!;

  public static byte GetMessageFailureCounter (Message message) => (byte)(message.FailureCounter is null? 0: message.FailureCounter);
}