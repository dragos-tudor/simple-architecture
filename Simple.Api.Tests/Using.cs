global using System.Collections.Generic;
global using System.Threading.Tasks;
global using Microsoft.AspNetCore.Builder;
global using Simple.Shared.Models;
global using Microsoft.AspNetCore.TestHost;
global using System.Net.Http.Json;
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Simple.Testing.Http.HttpFuncs;
global using static Simple.Testing.Models.ModelsFuncs;
global using static Simple.Api.ApiFuncs;

namespace Simple.Api;

[TestClass]
public partial class ApiTests
{
  static WebApplication ApiServer = default!;

  [AssemblyInitialize]
  public static void InitializeTests(TestContext _) =>
    ApiServer = RunSynchronously(() =>
      StartAppAsync([], "settings.json", (builder) => builder.WebHost.UseTestServer()));

  [AssemblyCleanup]
  public static void CleanupTests() =>
    RunSynchronously(() => ApiServer.StopAsync());
}