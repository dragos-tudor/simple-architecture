
namespace Simple.Web.Api;

partial class ApiFuncs
{
  // TODO: add exception type analysis
  static bool IsMessageActive (Message message, MessageHandlerOptions handlerOptions) => GetMessageFailureCounter(message) <= handlerOptions.MaxFailures;
}