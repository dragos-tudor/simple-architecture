
global using System.Net.Http.Json;
global using Microsoft.AspNetCore.TestHost;
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Docker.Extensions.DockerFuncs;
global using static Simple.Api.ApiFuncs;
global using static Simple.Domain.Models.ModelsFuncs;
global using static Simple.Infrastructure.MongoDb.MongoDbFuncs;
global using static Simple.Infrastructure.SqlServer.SqlServerFuncs;
global using static Simple.Shared.Testing.TestingFuncs;

using Microsoft.Extensions.Logging.Abstractions;

namespace Simple.Api;

[TestClass]
public partial class ApiTesting
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

    var loggerFactory = GetRequiredService<ILoggerFactory>(app.Services);
    var serverIntegrations = RunSynchronously(() => IntegrateServersAsync(configuration, RegisterMongoSubscribers, RegisterSqlSubscribers, loggerFactory, cancellationToken));
    IntegrateApi(app, serverIntegrations, loggerFactory);

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