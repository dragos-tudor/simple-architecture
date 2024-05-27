
namespace Simple.Web.Endpoints;

partial class EndpointsFuncs
{
  static IEnumerable<string> GetDispatchErrors (IEnumerable<Task<string?>> errors) => errors.Select(error => error.Result!).Where(ExistDispatchError);

  static string? GetMessageType<TMessage> (TMessage? message) => message is Message? (message as Message)?.MessageType: default;

  static string? GetMessageTraceId<TMessage> (TMessage? message) => message is Message? (message as Message)?.TraceId: default;
}