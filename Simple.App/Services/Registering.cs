
namespace Simple.App;

partial class AppFuncs
{
  static IServiceCollection RegisterLogging(IServiceCollection services, ILoggerFactory loggerFactory) => services.AddSingleton(loggerFactory);
}