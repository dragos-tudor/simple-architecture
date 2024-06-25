
namespace Simple.App;

partial class AppFuncs
{
  static IHost BuildHost (string[] args, IConfiguration configuration, Action<HostApplicationBuilder> configBuilder)
  {
    var builder = Host.CreateApplicationBuilder(args);
    RegisterLogging(builder.Services, IntegrateSerilog(configuration));
    configBuilder(builder);

    return builder.Build();
  }
}
