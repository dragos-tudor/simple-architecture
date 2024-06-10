
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Threading.Tasks;
global using Simple.Domain.Models;
global using Simple.Web.Api;
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using Microsoft.AspNetCore.TestHost;
global using static Simple.Web.Testing.TestingFuncs;
global using static Simple.Shared.Testing.TestingFuncs;

using System.Collections.Concurrent;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using static Docker.Extensions.DockerFuncs;


namespace Simple.Web.Testing;

[TestClass]
public partial class TestingFuncs
{
  static readonly ConcurrentBag<Notification> NotificationsStore = [];
  static WebApplication ApiServer = default!;

  [AssemblyInitialize]
  public static void InitializeApi (TestContext _)
  {
    var configPath = "/workspaces/simple-architecture/Simple.Web.Api/settings.json";
    var configuration = new ConfigurationBuilder().AddJsonFile(configPath).Build();
    var configBuilder = (WebApplicationBuilder builder) => {
      builder.WebHost.UseTestServer();
      builder.Services.AddSingleton<ILoggerFactory>(new NullLoggerFactory());
    };

    ApiServer = RunSynchronously(() => Program.StartupAppAsync(configuration, configBuilder, NotificationsStore.Add));
    ApiServer.RunAsync();
  }
}