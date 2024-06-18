
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Simple.Infrastructure.Queue;
using static Storing.MongoDb.MongoDbFuncs;

namespace Simple.App.Integrations;

partial class IntegrationFuncs
{
  public static async Task<(IMongoDatabase, Channel<Message>)> IntegrateMongoReplicaSetAsync (IConfiguration configuration, SendNotification<Notification> sendNotification, ILoggerFactory loggerFactory, CancellationToken appCancellationToken)
  {
    using var startCancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(10));
    var startCancellationToken = startCancellationTokenSource.Token;

    // https://github.com/dotnet/runtime/issues/83803
    var replicaSetOptions = SanitizeReplicaSetOptions(GetConfigurationOptions<MongoReplicaSetOptions>(configuration));
    await InitializeMongeReplicaSetAsync(replicaSetOptions, startCancellationToken);

    var mongoMessageQueue = CreateMessageQueue<Message>(1000);
    var mongoConnString = GetMongoConnectionString(string.Join(",", replicaSetOptions.ContainerNames), replicaSetOptions.ReplicaSet);
    var mongoDb = GetMongoDatabase(CreateMongoClient(mongoConnString), replicaSetOptions.DbName);

    var queueLogger = loggerFactory.CreateLogger(typeof(QueueFuncs).Namespace!);
    var messageHandlerOptions = GetConfigurationOptions<MessageHandlerOptions>(configuration);

    var mongoSubscribers = RegisterMongoSubscribers(TimeProvider.System, mongoDb, sendNotification, mongoMessageQueue, queueLogger);
    _ = DequeueMongoMessages(mongoMessageQueue, mongoSubscribers, mongoDb, messageHandlerOptions, queueLogger, appCancellationToken);

    return (mongoDb, mongoMessageQueue);
  }
}
