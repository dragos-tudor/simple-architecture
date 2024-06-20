
namespace Simple.Infrastructure.Notifications;

public delegate Task<IEnumerable<TNotification>> ReceiveNotifications<TNotification> (string userName, string password, Predicate<TNotification> filterNotification, CancellationToken cancellationToken = default);

public delegate Task SendNotifications<TNotification> (IEnumerable<TNotification> notifications, CancellationToken cancellationToken = default);