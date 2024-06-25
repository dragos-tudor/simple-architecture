
using System.Reflection;

namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  static Assembly GetMessagePayloadAssembly () => typeof(ModelsFuncs).Assembly;

  static Type GetMessagePayloadType (Assembly assembly, Message message) => assembly.GetType(message.MessageType)!;

  public static byte GetMessageFailureCounter (Message message) => (byte)(message.FailureCounter is null? 0: message.FailureCounter);
}