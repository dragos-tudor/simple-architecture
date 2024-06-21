
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading.Tasks;
global using Simple.Domain.Models;
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using Microsoft.AspNetCore.TestHost;
global using static Simple.Api.ApiFuncs;
global using static Simple.App.Integrations.IntegrationsFuncs;
global using static Simple.App.Services.ServicesFuncs;
global using static Simple.Domain.Models.ModelsFuncs;
global using static Simple.Infrastructure.EmailServer.EmailServerFuncs;
global using static Simple.Infrastructure.MongoDb.MongoDbFuncs;
global using static Simple.Infrastructure.SqlServer.SqlServerFuncs;
global using static Simple.Shared.Testing.TestingFuncs;
global using AgendaContextFactory = Microsoft.EntityFrameworkCore.Infrastructure.PooledDbContextFactory<Simple.Infrastructure.SqlServer.AgendaContext>;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Microsoft.Extensions.Logging.Abstractions;
using System.Threading;
using static Docker.Extensions.DockerFuncs;
using Simple.Infrastructure.EmailServer;

namespace Simple.Api.Testing;

[TestClass]
public partial class TestingFuncs
{
  static readonly CancellationTokenSource CancellationTokenSource = new (Timeout.Infinite);
  static WebApplication ApiServer = default!;
  static AgendaContextFactory AgendaContextFactory = default!;
  static IMongoDatabase AgendaDatabase = default!;
  static ReceiveMails<Notification> ReceiveNotifications = default!;

  [AssemblyInitialize]
  public static void InitializeTests (TestContext context)
  {
    var cancellationToken = CancellationTokenSource.Token;
    var configuration = BuildConfiguration("settings.json");
    var configBuilder = (WebApplicationBuilder builder) => {
      builder.WebHost.UseTestServer();
      builder.Services.AddSingleton<ILoggerFactory>(new NullLoggerFactory());
    };

    var app = BuildApplication([], configuration, configBuilder);
    var serverIntegrations = RunSynchronously(() => IntegrateServersAndApiAsync(app, configuration, cancellationToken));
    app.StartAsync(cancellationToken);

    ApiServer = app;
    AgendaContextFactory = serverIntegrations.SqlIntegration.SqlContextFactory;
    AgendaDatabase = serverIntegrations.MongoIntegration.MongoDatabase;
    ReceiveNotifications = (userName, password, filterNotification, cancellationToken) =>
      ReceiveMailsAsync(userName, password, GetEmailServerOptions(configuration), MapMessage<Notification>, filterNotification, cancellationToken);
  }

  [AssemblyCleanup]
  public static void CleanupTests ()
  {
    CancellationTokenSource.Cancel();
  }
}