
using Microsoft.Extensions.Configuration;

namespace Simple.App.Integrations;

partial class IntegrationsFuncs
{
  public static async Task<ServerIntegrations> IntegrateServersAsync (IConfiguration configuration, ILoggerFactory loggerFactory, CancellationToken cancellationToken = default)
  {
    var emailServerOptions = GetEmailServerOptions(configuration);
    var replicaSetOptions = GetMongoReplicaSetOptions(configuration);
    var sqlServerOptions = GetSqlServerOptions(configuration);
    var messageHandlerOptions = GetMessageHandlerOptions(configuration);
    SendNotification<Notification> sendNotification = (notification, cancellationToken) => SendMailAsync(notification, emailServerOptions, MapNotification, cancellationToken);

    var emailServerTask = IntegrateEmailServerAsync(emailServerOptions, cancellationToken);
    var mongoTask = IntegrateMongoReplicaSetAsync(replicaSetOptions, messageHandlerOptions, sendNotification, loggerFactory, cancellationToken);
    var sqlServerTask = IntegrateSqlServerAsync(sqlServerOptions, messageHandlerOptions, sendNotification, loggerFactory, cancellationToken);
    await Task.WhenAll([emailServerTask, mongoTask, sqlServerTask]);

    return new (await mongoTask, await sqlServerTask);
  }
}