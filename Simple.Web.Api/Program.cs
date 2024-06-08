
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder();

using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(10));
var cancellationToken = cancellationTokenSource.Token;

var sqlServerOptions = new SqlServerOptions (
  "sa", "admin.P@ssw0rd",
  "sqluser", "sqluser.P@ssw0rd",
  "mcr.microsoft.com/mssql/server:2019-latest", "simple-sql",
  "agenda-api", "simple-network",
  1433
);
var replicaSetOptions = new MongoReplicaSetOptions (
  "mongo:4.2.24", ["simple-mongo1", "simple-mongo2", "simple-mongo3"],
  "simple-network", "rs0",
  "agenda-api", ["contacts", "messages"],
  27017
);
await Task.WhenAll(
  InitializeSqlServerAsync(sqlServerOptions, cancellationToken),
  InitializeMongeReplicaSetAsync (replicaSetOptions, cancellationToken)
);


builder.Services.AddProblemDetails();
var app = builder.Build();
var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();
var logger = loggerFactory.CreateLogger(nameof(Simple.Web));

var notificationServerOptions = new NotificationServerOptions("localhost", 9025);
var shutdownNotificationServer = StartNotificationServer(notificationServerOptions, (notification) => LogSentNotification(logger, notification.From, notification.To, notification.Title));
var sendNotification = CreateNotificationSender(notificationServerOptions);

app.UseExceptionHandler().UseRouting();

var sqlMessageQueue = CreateMessageQueue<Message>(1000);
var sqlConnString = CreateSqlConnectionString("agenda-api", "sqluser", "sqluser.P@ssw0rd", "simple-sql");
var sqlDbContextFactory = new AgendaContextFactory(CreateSqlContextOptions<AgendaContext>(sqlConnString));
var sqlSubscribers = RegisterSqlSubscribers(TimeProvider.System, sqlDbContextFactory, sendNotification, sqlMessageQueue, logger);
_ = ConsumeSqlMessages(sqlMessageQueue, sqlSubscribers, sqlDbContextFactory, logger);
MapSqlEndpoints(app, sqlDbContextFactory, sqlMessageQueue, logger);

var mongoMessageQueue = CreateMessageQueue<Message>(1000);
var mongoConnString = GetMongoConnectionString("simple-mongo1,simple-mongo2,simple-mongo3", "rs0");
var mongoDb = GetMongoDatabase(CreateMongoClient(mongoConnString), "agenda-api");
var mongoSubscribers = RegisterMongoSubscribers(TimeProvider.System, mongoDb, sendNotification, mongoMessageQueue, logger);
_ = ConsumeMongoMessages(mongoMessageQueue, mongoSubscribers, mongoDb, logger);
MapMongoEndpoints(app, mongoDb, mongoMessageQueue, logger);

await app.RunAsync();
shutdownNotificationServer();
