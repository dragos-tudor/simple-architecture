
namespace Simple.Web.Api;

partial class ApiFuncs
{
  static string? GetMessageType<TMessage> (TMessage? message) => message is Message? (message as Message)?.MessageType: default;

  static string? GetMessageTraceId<TMessage> (TMessage? message) => message is Message? (message as Message)?.TraceId: default;
}