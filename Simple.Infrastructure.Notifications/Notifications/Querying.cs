
namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  public static IEnumerable<Notification> FindNotificationByTitle (IEnumerable<Notification> notifications, string title) => notifications.Where(notification => notification.Title == title);

  public static IEnumerable<Notification> GetNotificationByTo (IEnumerable<Notification> notifications, string to) => notifications.Where(notification => notification.To == to);
}