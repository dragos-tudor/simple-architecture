
namespace Simple.Messaging.Handlers;

partial class HandlersFuncs
{
  static bool ShouldRetryProcessMessage(byte errorCounter, byte maxErrors) => errorCounter < maxErrors;
}