
using Simple.Infrastructure.Queue;
using static Storing.SqlServer.SqlServerFuncs;

namespace Simple.Infrastructure.Integrations;

partial class IntegrationsFuncs
{
  public static async Task<SqlIntegration> IntegrateSqlServerAsync (
    SqlServerOptions sqlServerOptions, MessageHandlerOptions messageHandlerOptions,
    EmailServerOptions emailServerOptions, RegisterSqlSubscribers registerSqlSubscribers, ILoggerFactory loggerFactory,
    CancellationToken appCancellationToken)
  {
    using var startCancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(10));
    var startCancellationToken = startCancellationTokenSource.Token;
    await InitializeSqlServerAsync(sqlServerOptions, startCancellationToken);

    var sqlMessageQueue = CreateMessageQueue<Message>(1000);
    var sqlConnString = CreateSqlServerConnectionString(sqlServerOptions);
    var sqlContextFactory = new AgendaContextFactory(CreateSqlContextOptions<AgendaContext>(sqlConnString));

    var queueLogger = loggerFactory.CreateLogger(typeof(QueueFuncs).Namespace!);
    var sqlSubscribers = registerSqlSubscribers(sqlContextFactory, emailServerOptions, sqlMessageQueue, queueLogger);
    _ = DequeueSqlMessages(sqlMessageQueue, sqlSubscribers, sqlContextFactory, messageHandlerOptions, queueLogger, appCancellationToken);

    return new (sqlContextFactory, sqlMessageQueue);
  }
}
