
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static string SetMessageErrorMessage(Message message, string errorMessage) => message.ErrorMessage = errorMessage;

  public static byte? SetMessageErrorCounter(Message message, byte errorCounter) => message.ErrorCounter = errorCounter;

  public static bool SetMessageIsPending(Message message, bool isPending) => message.IsPending = isPending;
}