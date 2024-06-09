
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Simple.Infrastructure.Queue;


var builder = WebApplication.CreateBuilder();
builder.Services.AddProblemDetails();

var app = builder.Build();
app.UseExceptionHandler().UseRouting();

using var startCancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(10));
var startCancellationToken = startCancellationTokenSource.Token;

var sqlServerOptions = builder.Configuration.GetSection(nameof(SqlServerOptions)).Get<SqlServerOptions>()!;
var replicaSetOptions = builder.Configuration.GetSection(nameof(MongoReplicaSetOptions)).Get<MongoReplicaSetOptions>()!;
// https://github.com/dotnet/runtime/issues/83803
replicaSetOptions = replicaSetOptions with { CollNames = replicaSetOptions.CollNames.Distinct().ToArray(), ContainerNames = replicaSetOptions.ContainerNames.Distinct().ToArray() };

await Task.WhenAll(
  InitializeSqlServerAsync(sqlServerOptions, startCancellationToken),
  InitializeMongeReplicaSetAsync (replicaSetOptions, startCancellationToken)
);



using var apiCancellationTokenSource = new CancellationTokenSource(Timeout.Infinite);
var apiCancellationToken = apiCancellationTokenSource.Token;

var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
var domainLogger = loggerFactory.CreateLogger(typeof(ServicesFuncs).Namespace!);
var queueLogger = loggerFactory.CreateLogger(typeof(QueueFuncs).Namespace!);
var notificationLogger = loggerFactory.CreateLogger(typeof(NotificationsFuncs).Namespace!);

var notificationServerOptions = builder.Configuration.GetSection(nameof(NotificationServerOptions)).Get<NotificationServerOptions>()!;
var shutdownNotificationServer = StartNotificationServer(notificationServerOptions, (notification) => LogSentNotification(notificationLogger, notification.From, notification.To, notification.Title));
var sendNotification = CreateNotificationSender(notificationServerOptions);

var sqlMessageQueue = CreateMessageQueue<Message>(1000);
var sqlConnString = CreateSqlConnectionString(sqlServerOptions.DbName, sqlServerOptions.UserName, sqlServerOptions.UserPassword, sqlServerOptions.ContainerName);
var sqlDbContextFactory = new AgendaContextFactory(CreateSqlContextOptions<AgendaContext>(sqlConnString));
var sqlSubscribers = RegisterSqlSubscribers(TimeProvider.System, sqlDbContextFactory, sendNotification, sqlMessageQueue, queueLogger);
_ = ConsumeSqlMessages(sqlMessageQueue, sqlSubscribers, sqlDbContextFactory, queueLogger, apiCancellationToken);
MapSqlEndpoints(app, sqlDbContextFactory, sqlMessageQueue, domainLogger);

var mongoMessageQueue = CreateMessageQueue<Message>(1000);
var mongoConnString = GetMongoConnectionString(string.Join(",", replicaSetOptions.ContainerNames), replicaSetOptions.ReplicaSet);
var mongoDb = GetMongoDatabase(CreateMongoClient(mongoConnString), replicaSetOptions.DbName);
var mongoSubscribers = RegisterMongoSubscribers(TimeProvider.System, mongoDb, sendNotification, mongoMessageQueue, queueLogger);
_ = ConsumeMongoMessages(mongoMessageQueue, mongoSubscribers, mongoDb, queueLogger, apiCancellationToken);
MapMongoEndpoints(app, mongoDb, mongoMessageQueue, domainLogger);

app.Lifetime.ApplicationStopping.Register(() => shutdownNotificationServer());
app.Lifetime.ApplicationStopping.Register(() => apiCancellationTokenSource.Cancel());
await app.RunAsync();
