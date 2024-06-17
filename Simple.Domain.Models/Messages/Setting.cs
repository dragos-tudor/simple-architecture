
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static string SetMessageFailureMessage (Message message, string failureMessage) => message.FailureMessage = failureMessage;

  public static byte? SetMessageFailureCounter (Message message, byte failureCounter) => message.FailureCounter = failureCounter;

  public static bool SetMessageIsActive (Message message, bool isActive) => message.IsActive = isActive;
}