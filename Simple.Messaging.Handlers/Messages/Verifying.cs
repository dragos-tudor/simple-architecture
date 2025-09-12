
namespace Simple.Messaging.Handlers;

partial class HandlersFuncs
{
  static bool ShouldRetryProcessMessage(Message message, byte maxErrors) => GetMessageErrorCounter(message) < maxErrors;
}