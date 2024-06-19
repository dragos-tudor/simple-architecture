
using Microsoft.Extensions.DependencyInjection;

namespace Simple.App.Services;

partial class ServicesFuncs
{
  public static void Main(string[] args)
  {
    var builder = Host.CreateApplicationBuilder(args);
    builder.Services.AddHostedService<JobSchedulerService>();

    var host = builder.Build();
    host.Run();
  }
}
