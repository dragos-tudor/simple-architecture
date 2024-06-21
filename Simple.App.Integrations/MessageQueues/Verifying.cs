
namespace Simple.App.Integrations;

partial class IntegrationsFuncs
{
  // TODO: add exception type analysis
  static bool IsActiveMessage (Message message, Exception exception, byte maxFailures) => GetMessageFailureCounter(message) <= maxFailures;
}