
using Microsoft.Extensions.Configuration;

namespace Simple.Infrastructure.Integrations;

partial class IntegrationsFuncs
{
  public static async Task<ServerIntegrations> IntegrateServersAsync (
    IConfiguration configuration, RegisterMongoSubscribers registerMongoSubscribers,
    RegisterSqlSubscribers registerSqlSubscribers, ILoggerFactory loggerFactory,
    CancellationToken cancellationToken = default)
  {
    var emailServerOptions = GetEmailServerOptions(configuration);
    var replicaSetOptions = GetMongoReplicaSetOptions(configuration);
    var sqlServerOptions = GetSqlServerOptions(configuration);
    var messageHandlerOptions = GetMessageHandlerOptions(configuration);

    var emailServerTask = IntegrateEmailServerAsync(emailServerOptions, cancellationToken);
    var mongoTask = IntegrateMongoReplicaSetAsync(replicaSetOptions, messageHandlerOptions, emailServerOptions, registerMongoSubscribers, loggerFactory, cancellationToken);
    var sqlServerTask = IntegrateSqlServerAsync(sqlServerOptions, messageHandlerOptions, emailServerOptions, registerSqlSubscribers, loggerFactory, cancellationToken);
    await Task.WhenAll([emailServerTask, mongoTask, sqlServerTask]);

    return new (await mongoTask, await sqlServerTask);
  }
}