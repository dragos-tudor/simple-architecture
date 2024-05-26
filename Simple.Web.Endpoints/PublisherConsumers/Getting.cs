
namespace Simple.Web.Endpoints;

partial class EndpointsFuncs
{
  internal static bool IsNotNullOrEmpty (string value) => !string.IsNullOrEmpty(value);

  static IEnumerable<string> GetDispatchErrors (IEnumerable<Task<string?>> errors) => errors.Select(error => error.Result).Where(IsNotNullOrEmpty);

  static string GetMessageType<TMessage> (TMessage message) => message!.ToString()!;

}