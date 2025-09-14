global using System.Net;
global using System.Net.Http;
global using System.Threading;
global using System.Collections.Generic;
global using System.Threading.Tasks;
global using System.Net.Http.Json;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.TestHost;
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using Simple.Shared.Models;
global using static Simple.Testing.Http.HttpFuncs;
global using static Simple.Testing.Models.ModelsFuncs;
global using static Simple.Api.ApiFuncs;

namespace Simple.Api;

[TestClass]
public partial class ApiTests
{
  static WebApplication ApiServer = default!;
  static readonly CancellationTokenSource CancellationTokenSource = new(Timeout.Infinite);

  [AssemblyInitialize]
  public static void InitializeTests(TestContext _)
  {
    var cancellationToken = CancellationTokenSource.Token;
    ApiServer = InitializeApiServer([], "settings.json", (builder) => builder.WebHost.UseTestServer(), cancellationToken);
    RunSynchronously(() => ApiServer.StartAsync());
  }

  [AssemblyCleanup]
  public static void CleanupTests()
  {
    CancellationTokenSource.Cancel();
    RunSynchronously(() => ApiServer.StopAsync());
  }
}