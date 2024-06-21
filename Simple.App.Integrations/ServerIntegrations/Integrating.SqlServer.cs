
using Simple.Infrastructure.Queue;
using static Storing.SqlServer.SqlServerFuncs;

namespace Simple.App.Integrations;

partial class IntegrationsFuncs
{
  public static async Task<SqlIntegration> IntegrateSqlServerAsync (SqlServerOptions sqlServerOptions, MessageHandlerOptions messageHandlerOptions, SendNotification<Notification> sendNotification, ILoggerFactory loggerFactory, CancellationToken appCancellationToken)
  {
    using var startCancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(10));
    var startCancellationToken = startCancellationTokenSource.Token;
    await InitializeSqlServerAsync(sqlServerOptions, startCancellationToken);

    var sqlMessageQueue = CreateMessageQueue<Message>(1000);
    var sqlConnString = CreateSqlServerConnectionString(sqlServerOptions);
    var agendaContextFactory = new AgendaContextFactory(CreateSqlContextOptions<AgendaContext>(sqlConnString));

    var queueLogger = loggerFactory.CreateLogger(typeof(QueueFuncs).Namespace!);
    var sqlSubscribers = RegisterSqlSubscribers(TimeProvider.System, agendaContextFactory, sendNotification, sqlMessageQueue, queueLogger);
    _ = DequeueSqlMessages(sqlMessageQueue, sqlSubscribers, agendaContextFactory, messageHandlerOptions, queueLogger, appCancellationToken);

    return new (agendaContextFactory, sqlMessageQueue);
  }
}
