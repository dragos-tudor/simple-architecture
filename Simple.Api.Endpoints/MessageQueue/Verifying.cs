
namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  static bool ShouldRetryProcessMessage(Message message, byte maxErrors) => GetMessageErrorCounter(message) <= maxErrors;
}