
global using System.Net;
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
public partial class ApiTests
{
  static WebApplication ApiServer = default!;
  static AgendaContextFactory SqlContextFactory = default!;
  static IMongoDatabase MongoDatabase = default!;
  static IConfiguration Configuration = default!;
  static readonly CancellationTokenSource CancellationTokenSource = new (Timeout.Infinite);

  [AssemblyInitialize]
  public static void InitializeTests (TestContext context)
  {
    var configuration = BuildConfiguration("settings.tests.json");
    var configBuilder = (WebApplicationBuilder builder) => {
      builder.WebHost.UseTestServer();
      builder.Services.AddSingleton<ILoggerFactory>(new NullLoggerFactory());
    };
    var app = BuildApplication([], configuration, configBuilder);

    var cancellationToken = CancellationTokenSource.Token;
    var loggerFactory = GetRequiredService<ILoggerFactory>(app.Services);
    var serverIntegrations = RunSynchronously(() => IntegrateServersAsync(configuration, RegisterMongoSubscribers, RegisterSqlSubscribers, loggerFactory, cancellationToken));
    MapEndpoints(app, serverIntegrations, loggerFactory);
    RunSynchronously(() => app.StartAsync(cancellationToken));

    ApiServer = app;
    SqlContextFactory = serverIntegrations.SqlIntegration.SqlContextFactory;
    MongoDatabase = serverIntegrations.MongoIntegration.MongoDatabase;
    Configuration = configuration;
  }

  [AssemblyCleanup]
  public static void CleanupTests ()
  {
    RunSynchronously(() => ApiServer.StopAsync());
    CancellationTokenSource.Cancel();
  }
}