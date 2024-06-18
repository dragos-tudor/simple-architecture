
using Microsoft.Extensions.Configuration;
using Simple.Infrastructure.Queue;
using static Storing.SqlServer.SqlServerFuncs;

namespace Simple.App.Integrations;

partial class IntegrationFuncs
{
  public static async Task<(AgendaContextFactory, Channel<Message>)> IntegrateSqlServerAsync (IConfiguration configuration, SendNotification<Notification> sendNotification, ILoggerFactory loggerFactory, CancellationToken appCancellationToken)
  {
    using var startCancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(10));
    var startCancellationToken = startCancellationTokenSource.Token;

    var sqlServerOptions = GetConfigurationOptions<SqlServerOptions>(configuration);
    await InitializeSqlServerAsync(sqlServerOptions, startCancellationToken);

    var sqlMessageQueue = CreateMessageQueue<Message>(1000);
    var sqlConnString = CreateSqlConnectionString(sqlServerOptions.DbName, sqlServerOptions.UserName, sqlServerOptions.UserPassword, sqlServerOptions.ContainerName);
    var sqlDbContextFactory = new AgendaContextFactory(CreateSqlContextOptions<AgendaContext>(sqlConnString));

    var queueLogger = loggerFactory.CreateLogger(typeof(QueueFuncs).Namespace!);
    var messageHandlerOptions = GetConfigurationOptions<MessageHandlerOptions>(configuration);

    var sqlSubscribers = RegisterSqlSubscribers(TimeProvider.System, sqlDbContextFactory, sendNotification, sqlMessageQueue, queueLogger);
    _ = DequeueSqlMessages(sqlMessageQueue, sqlSubscribers, sqlDbContextFactory, messageHandlerOptions, queueLogger, appCancellationToken);

    return (sqlDbContextFactory, sqlMessageQueue);
  }
}
