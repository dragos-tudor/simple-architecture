
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Simple.Infrastructure.Queue;
using static Storing.SqlServer.SqlServerFuncs;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  public static async Task<AgendaContextFactory> IntegrateSqlServerAsync (WebApplication app, SendNotification<Notification> sendNotification, ILoggerFactory loggerFactory, CancellationToken appCancellationToken)
  {
    using var startCancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(10));
    var startCancellationToken = startCancellationTokenSource.Token;

    var sqlServerOptions = app.Configuration.GetSection(nameof(SqlServerOptions)).Get<SqlServerOptions>()!;
    await InitializeSqlServerAsync(sqlServerOptions, startCancellationToken);

    var sqlMessageQueue = CreateMessageQueue<Message>(1000);
    var sqlConnString = CreateSqlConnectionString(sqlServerOptions.DbName, sqlServerOptions.UserName, sqlServerOptions.UserPassword, sqlServerOptions.ContainerName);
    var sqlDbContextFactory = new AgendaContextFactory(CreateSqlContextOptions<AgendaContext>(sqlConnString));

    var domainLogger = loggerFactory.CreateLogger(typeof(ServicesFuncs).Namespace!);
    var queueLogger = loggerFactory.CreateLogger(typeof(QueueFuncs).Namespace!);

    var sqlSubscribers = RegisterSqlSubscribers(TimeProvider.System, sqlDbContextFactory, sendNotification, sqlMessageQueue, queueLogger);
    _ = ConsumeSqlMessages(sqlMessageQueue, sqlSubscribers, sqlDbContextFactory, queueLogger, appCancellationToken);

    MapSqlEndpoints(app, sqlDbContextFactory, sqlMessageQueue, domainLogger);
    return sqlDbContextFactory;
  }
}
