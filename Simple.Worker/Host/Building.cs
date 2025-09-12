
namespace Simple.Worker;

partial class WorkerFuncs
{
  static IHost BuildHost(string[] args, IConfiguration configuration, Action<HostApplicationBuilder> configBuilder)
  {
    var builder = Host.CreateApplicationBuilder(args);
    AddConfiguration(builder.Configuration, configuration);
    RegisterServices(builder.Services);
    configBuilder(builder);

    return builder.Build();
  }
}
