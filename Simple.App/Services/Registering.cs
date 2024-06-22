
namespace Simple.App;

partial class AppFuncs
{
  static IServiceCollection RegisterLogging(IServiceCollection services, ILoggerFactory loggerFactory) => services.AddSingleton(loggerFactory);

  static IServiceCollection RegisterHostServices (IServiceCollection services) => services.AddHostedService<JobSchedulerService>();

  static IServiceCollection RegisterServices (IServiceCollection services) => services.AddProblemDetails();

}