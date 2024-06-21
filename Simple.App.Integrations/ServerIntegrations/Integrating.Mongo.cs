
using Simple.Infrastructure.Queue;

namespace Simple.App.Integrations;

partial class IntegrationsFuncs
{
  public static async Task<MongoIntegration> IntegrateMongoReplicaSetAsync (MongoReplicaSetOptions replicaSetOptions, MessageHandlerOptions messageHandlerOptions, SendNotification<Notification> sendNotification, ILoggerFactory loggerFactory, CancellationToken appCancellationToken)
  {
    using var startCancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(10));
    var startCancellationToken = startCancellationTokenSource.Token;
    await InitializeMongeReplicaSetAsync(replicaSetOptions, startCancellationToken);

    var mongoMessageQueue = CreateMessageQueue<Message>(1000);
    var mongoDatabase = GetMongoDatabase(replicaSetOptions);

    var queueLogger = loggerFactory.CreateLogger(typeof(QueueFuncs).Namespace!);
    var mongoSubscribers = RegisterMongoSubscribers(TimeProvider.System, mongoDatabase, sendNotification, mongoMessageQueue, queueLogger);
    _ = DequeueMongoMessages(mongoMessageQueue, mongoSubscribers, mongoDatabase, messageHandlerOptions, queueLogger, appCancellationToken);

    return new MongoIntegration(mongoDatabase, mongoMessageQueue);
  }
}
