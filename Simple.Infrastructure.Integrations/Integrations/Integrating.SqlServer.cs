
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
    var agendaContextFactory = new AgendaContextFactory(CreateSqlContextOptions<AgendaContext>(sqlConnString));

    var queueLogger = loggerFactory.CreateLogger(typeof(QueueFuncs).Namespace!);
    var sqlSubscribers = registerSqlSubscribers(agendaContextFactory, emailServerOptions, sqlMessageQueue, queueLogger);
    _ = DequeueSqlMessages(sqlMessageQueue, sqlSubscribers, agendaContextFactory, messageHandlerOptions, queueLogger, appCancellationToken);

    return new (agendaContextFactory, sqlMessageQueue);
  }
}
