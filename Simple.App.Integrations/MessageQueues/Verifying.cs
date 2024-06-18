
namespace Simple.App.Integrations;

partial class IntegrationFuncs
{
  // TODO: add exception type analysis
  static bool IsActiveMessage (Message message, Exception exception, MessageHandlerOptions handlerOptions) => GetMessageFailureCounter(message) <= handlerOptions.MaxFailures;
}