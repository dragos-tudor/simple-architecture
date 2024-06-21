
namespace Simple.App;

partial class AppFuncs
{
  static void RegisterHostServices (IServiceCollection services) => services.AddHostedService<JobSchedulerService>();
}