
using System.Threading;
using System.Threading.Tasks;

namespace Simple.Domain.Models;

public delegate Task SendNotification<TNotification> (TNotification notification, CancellationToken cancellationToken = default) where TNotification: Notification;