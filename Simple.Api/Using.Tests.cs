
global using Microsoft.AspNetCore.TestHost;
global using System.Net.Http.Json;
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Simple.Api.ApiFuncs;
global using static Simple.Shared.Testing.TestingFuncs;

using Microsoft.Extensions.Logging.Abstractions;
using static Docker.Extensions.DockerFuncs;

namespace Simple.Api;

[TestClass]
public partial class ApiTests
{
  static WebApplication ApiServer = default!;
  static readonly CancellationTokenSource CancellationTokenSource = new (TimeSpan.FromMinutes(10));

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
  }

  [AssemblyCleanup]
  public static void CleanupTests ()
  {
    RunSynchronously(() => ApiServer.StopAsync());
    CancellationTokenSource.Cancel();
  }
}