
namespace Simple.Api;

partial class ApiFuncs
{
  static IServiceCollection RegisterLogging(IServiceCollection services, ILoggerFactory loggerFactory) => services.AddSingleton(loggerFactory);

  static IServiceCollection RegisterServices (IServiceCollection services) => services.AddProblemDetails();
}