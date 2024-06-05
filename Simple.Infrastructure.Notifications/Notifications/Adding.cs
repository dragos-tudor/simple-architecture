
namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  public static bool AddNotification<TNotification> (IProducerConsumerCollection<TNotification> notifications, TNotification notification) => notifications.TryAdd(notification);
}