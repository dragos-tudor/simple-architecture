
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading;
global using System.Threading.Tasks;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Logging.Abstractions;
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using Microsoft.AspNetCore.TestHost;
global using Simple.Domain.Models;
global using static Simple.Api.ApiFuncs;
global using static Simple.App.Services.ServicesFuncs;
global using static Simple.Domain.Models.ModelsFuncs;
global using static Simple.Infrastructure.Integrations.IntegrationsFuncs;
global using static Simple.Infrastructure.MongoDb.MongoDbFuncs;
global using static Simple.Infrastructure.SqlServer.SqlServerFuncs;
global using static Simple.Shared.Testing.TestingFuncs;
global using AgendaContextFactory = Microsoft.EntityFrameworkCore.Infrastructure.PooledDbContextFactory<Simple.Infrastructure.SqlServer.AgendaContext>;

using MongoDB.Driver;
using static Docker.Extensions.DockerFuncs;

namespace Simple.Api.Testing;

[TestClass]
public partial class TestingFuncs
{
  static readonly CancellationTokenSource CancellationTokenSource = new (Timeout.Infinite);
  static WebApplication ApiServer = default!;
  static AgendaContextFactory AgendaContextFactory = default!;
  static IMongoDatabase AgendaDatabase = default!;
  static Func<string, string, Predicate<Notification>, Task<IEnumerable<Notification>>> ReceiveNotifications = default!;

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
    ReceiveNotifications = (userName, password, filterNotification) =>
      ReceiveNotificationsAsync(userName, password, GetEmailServerOptions(configuration), filterNotification);
  }

  [AssemblyCleanup]
  public static void CleanupTests ()
  {
    CancellationTokenSource.Cancel();
  }
}