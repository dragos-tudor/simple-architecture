
namespace Simple.Jobs;

partial class JobsFuncs
{
  static IServiceCollection RegisterLogging(IServiceCollection services, ILoggerFactory loggerFactory) => services.AddSingleton(loggerFactory);
}