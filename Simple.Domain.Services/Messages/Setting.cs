
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static Message SetMessageTraceId (Message message, string traceId) { message.TraceId = traceId; return message; }
}