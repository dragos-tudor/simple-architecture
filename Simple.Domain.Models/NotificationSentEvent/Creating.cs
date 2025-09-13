
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static NotificationSentEvent CreateNotificationSentEvent(string from, string to, string subject, string content, DateTime date) =>
    new(from, to, subject, content, date);
}