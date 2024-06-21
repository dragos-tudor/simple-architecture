
namespace Simple.App;

partial class AppFuncs
{
  static IHost BuildHost (string[] args, IConfiguration configuration, Action<HostApplicationBuilder> configBuilder)
  {
    var builder = Host.CreateApplicationBuilder(args);
    RegisterHostServices(builder.Services);
    RegisterLogging(builder.Services, IntegrateSerilog(configuration));
    RegisterServices(builder.Services);
    configBuilder(builder);

    return builder.Build();
  }
}
