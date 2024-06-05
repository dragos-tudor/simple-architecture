
namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  internal static Notification CreateNotification (string from, string to, string title, string content, DateTimeOffset date) =>
    new () { From = from, To = to, Title = title, Content = content, Date = date};
}