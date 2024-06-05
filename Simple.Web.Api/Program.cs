
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Builder;

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

var app = builder.Build();

var notifications = new ConcurrentBag<Notification>();
var shutdownServer = StartNotificationServer("localhost", 9025, notifications);
var sendNotification = CreateNotificationSender("localhost", 9025);

var sqlMessageQueue = CreateMessageQueue<Message>(1000);
var sqlConnString = CreateSqlConnectionString("agenda-api", "sqluser", "sqluser.P@ssw0rd", "simple-sql");
var sqlDbContextFactory = new AgendaContextFactory(CreateSqlContextOptions<AgendaContext>(sqlConnString));
var sqlSubscribers = RegisterSqlSubscribers(TimeProvider.System, sqlDbContextFactory, sendNotification, sqlMessageQueue);
MapSqlEndpoints(app, sqlDbContextFactory, sqlMessageQueue);
ConsumeMessages(sqlMessageQueue, sqlSubscribers, async (message, cancellationToken) => {
  using var agendaContext = await sqlDbContextFactory.CreateDbContextAsync(cancellationToken);
  UpdateMessageIsActive(agendaContext, message, false);
  await SaveChanges(agendaContext, cancellationToken);
  return true;
});

var mongoMessageQueue = CreateMessageQueue<Message>(1000);
var mongoConnString = GetMongoConnectionString("simple-mongo1,simple-mongo2,simple-mongo3", "rs0");
var mongoDb = GetMongoDatabase(CreateMongoClient(mongoConnString), "agenda-api");
var mongoSubscribers = RegisterSqlSubscribers(TimeProvider.System, sqlDbContextFactory, sendNotification, sqlMessageQueue);
MapMongoEndpoints(app, mongoDb, mongoMessageQueue);
ConsumeMessages(mongoMessageQueue, mongoSubscribers, async (message, cancellationToken) => {
  var messages = GetMessageCollection(mongoDb);
  await UpdateMessageIsActive(messages, message, false, cancellationToken);
  return true;
});

app.UseRouting();
await app.RunAsync();
shutdownServer();
