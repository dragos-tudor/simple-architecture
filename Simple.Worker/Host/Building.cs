
namespace Simple.Worker;

partial class WorkerFuncs
{
  internal static IHost BuildHost (string[] args, IConfiguration configuration, Action<HostApplicationBuilder> configBuilder)
  {
    var builder = Host.CreateApplicationBuilder(args);
    RegisterLogging(builder.Services, IntegrateSerilog(configuration));
    AddConfiguration(builder.Configuration, configuration);
    configBuilder(builder);

    return builder.Build();
  }
}
