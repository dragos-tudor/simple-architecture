
namespace Simple.Worker.Jobs;

partial class JobsFuncs
{
  static bool ShouldRetryProcessMessage(Message message, byte maxErrors) => GetMessageErrorCounter(message) <= maxErrors;
}