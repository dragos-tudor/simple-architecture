
namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  internal static Notification CreateNotification (string from, string to, string title, string content, DateTimeOffset date) =>
    new (from, to, title, content, date);
}