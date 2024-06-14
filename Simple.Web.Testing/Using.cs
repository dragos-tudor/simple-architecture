
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading.Tasks;
global using Simple.Domain.Models;
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using Microsoft.AspNetCore.TestHost;
global using static Simple.Domain.Models.ModelsFuncs;
global using static Simple.Infrastructure.MongoDb.MongoDbFuncs;
global using static Simple.Infrastructure.SqlServer.SqlServerFuncs;
global using static Simple.Shared.Testing.TestingFuncs;
global using static Simple.Web.Api.ApiFuncs;
global using AgendaContextFactory = Microsoft.EntityFrameworkCore.Infrastructure.PooledDbContextFactory<Simple.Infrastructure.SqlServer.AgendaContext>;

using System.Collections.Concurrent;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static Docker.Extensions.DockerFuncs;
using MongoDB.Driver;

namespace Simple.Web.Testing;

[TestClass]
public partial class TestingFuncs
{
  static WebApplication ApiServer = default!;
  static AgendaContextFactory AgendaContextFactory = default!;
  static IMongoDatabase AgendaDb = default!;
  static readonly ConcurrentBag<Notification> NotificationStore = [];

  [AssemblyInitialize]
  public static void InitializeTests (TestContext context)
  {
    var configuration = BuildConfiguration("settings.json");
    var configBuilder = (WebApplicationBuilder builder) => {
      builder.WebHost.UseTestServer();
      builder.Logging.ClearProviders();
    };

    (ApiServer, AgendaContextFactory, AgendaDb) = RunSynchronously(() => StartupAppAsync(configuration, configBuilder, NotificationStore.Add));
    ApiServer.RunAsync();
  }

  [AssemblyCleanup]
  public static void CleanupTests ()
  {
    ApiServer?.StopAsync();
  }
}