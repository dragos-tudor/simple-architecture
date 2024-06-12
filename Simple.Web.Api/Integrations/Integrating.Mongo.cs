
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Simple.Infrastructure.Queue;
using static Storing.MongoDb.MongoDbFuncs;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static async Task<IMongoDatabase> IntegrateMongoReplicaSetAsync (WebApplication app, SendNotification<Notification> sendNotification, ILoggerFactory loggerFactory, CancellationToken appCancellationToken)
  {
    using var startCancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(10));
    var startCancellationToken = startCancellationTokenSource.Token;

    var replicaSetOptions = app.Configuration.GetSection(nameof(MongoReplicaSetOptions)).Get<MongoReplicaSetOptions>()!;
    // https://github.com/dotnet/runtime/issues/83803
    replicaSetOptions = replicaSetOptions with { CollNames = replicaSetOptions.CollNames.Distinct().ToArray(), ContainerNames = replicaSetOptions.ContainerNames.Distinct().ToArray() };
    await InitializeMongeReplicaSetAsync(replicaSetOptions, startCancellationToken);

    var mongoMessageQueue = CreateMessageQueue<Message>(1000);
    var mongoConnString = GetMongoConnectionString(string.Join(",", replicaSetOptions.ContainerNames), replicaSetOptions.ReplicaSet);
    var mongoDb = GetMongoDatabase(CreateMongoClient(mongoConnString), replicaSetOptions.DbName);

    var domainLogger = loggerFactory.CreateLogger(typeof(ServicesFuncs).Namespace!);
    var queueLogger = loggerFactory.CreateLogger(typeof(QueueFuncs).Namespace!);

    var mongoSubscribers = RegisterMongoSubscribers(TimeProvider.System, mongoDb, sendNotification, mongoMessageQueue, queueLogger);
    _ = ConsumeMongoMessages(mongoMessageQueue, mongoSubscribers, mongoDb, queueLogger, appCancellationToken);

    MapMongoEndpoints(app, mongoDb, mongoMessageQueue, domainLogger);
    return mongoDb;
  }
}
