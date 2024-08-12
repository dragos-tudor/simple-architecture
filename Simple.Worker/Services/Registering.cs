
namespace Simple.Worker;

partial class WorkerFuncs
{
  static IServiceCollection RegisterLogging(IServiceCollection services, ILoggerFactory loggerFactory) => services.AddSingleton(loggerFactory);
}