
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static Message SetMessageTraceId (Message message, string traceId) { message.TraceId = traceId; return message; }
}