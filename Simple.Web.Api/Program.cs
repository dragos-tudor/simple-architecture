
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder();
builder.Services.AddProblemDetails();

var app = builder.Build();
app.UseExceptionHandler().UseRouting();

using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(10));
var cancellationToken = cancellationTokenSource.Token;

var sqlServerOptions = builder.Configuration.GetSection(nameof(SqlServerOptions)).Get<SqlServerOptions>()!;
var replicaSetOptions = builder.Configuration.GetSection(nameof(MongoReplicaSetOptions)).Get<MongoReplicaSetOptions>()!;
// https://github.com/dotnet/runtime/issues/83803
replicaSetOptions = replicaSetOptions with { CollNames = replicaSetOptions.CollNames.Distinct().ToArray(), ContainerNames = replicaSetOptions.ContainerNames.Distinct().ToArray() };

await Task.WhenAll(
  InitializeSqlServerAsync(sqlServerOptions, cancellationToken),
  InitializeMongeReplicaSetAsync (replicaSetOptions, cancellationToken)
);

var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
var logger = loggerFactory.CreateLogger(nameof(Simple.Web));

var notificationServerOptions = builder.Configuration.GetSection(nameof(NotificationServerOptions)).Get<NotificationServerOptions>()!;
var shutdownNotificationServer = StartNotificationServer(notificationServerOptions, (notification) => LogSentNotification(logger, notification.From, notification.To, notification.Title));
var sendNotification = CreateNotificationSender(notificationServerOptions);

var sqlMessageQueue = CreateMessageQueue<Message>(1000);
var sqlConnString = CreateSqlConnectionString(sqlServerOptions.DbName, sqlServerOptions.UserName, sqlServerOptions.UserPassword, sqlServerOptions.ContainerName);
var sqlDbContextFactory = new AgendaContextFactory(CreateSqlContextOptions<AgendaContext>(sqlConnString));
var sqlSubscribers = RegisterSqlSubscribers(TimeProvider.System, sqlDbContextFactory, sendNotification, sqlMessageQueue, logger);
_ = ConsumeSqlMessages(sqlMessageQueue, sqlSubscribers, sqlDbContextFactory, logger);
MapSqlEndpoints(app, sqlDbContextFactory, sqlMessageQueue, logger);

var mongoMessageQueue = CreateMessageQueue<Message>(1000);
var mongoConnString = GetMongoConnectionString(string.Join(",", replicaSetOptions.ContainerNames), replicaSetOptions.ReplicaSet);
var mongoDb = GetMongoDatabase(CreateMongoClient(mongoConnString), replicaSetOptions.DbName);
var mongoSubscribers = RegisterMongoSubscribers(TimeProvider.System, mongoDb, sendNotification, mongoMessageQueue, logger);
_ = ConsumeMongoMessages(mongoMessageQueue, mongoSubscribers, mongoDb, logger);
MapMongoEndpoints(app, mongoDb, mongoMessageQueue, logger);

await app.RunAsync();
shutdownNotificationServer();
