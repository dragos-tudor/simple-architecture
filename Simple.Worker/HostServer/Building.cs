
namespace Simple.Worker;

partial class WorkerFuncs
{
  static IHost BuildHostServer(string[] args, IConfiguration configuration, Action<HostApplicationBuilder> configBuilder)
  {
    var hostServerBuilder = Host.CreateApplicationBuilder(args);
    AddConfiguration(hostServerBuilder.Configuration, configuration);
    RegisterServices(hostServerBuilder.Services);
    configBuilder(hostServerBuilder);

    return hostServerBuilder.Build();
  }
}
