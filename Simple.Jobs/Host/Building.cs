
namespace Simple.Jobs;

partial class JobsFuncs
{
  internal static IHost BuildHost (string[] args, IConfiguration configuration, Action<HostApplicationBuilder> configBuilder)
  {
    var builder = Host.CreateApplicationBuilder(args);
    RegisterLogging(builder.Services, IntegrateSerilog(configuration));
    configBuilder(builder);

    return builder.Build();
  }
}
