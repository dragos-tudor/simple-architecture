
using Microsoft.Extensions.DependencyInjection;

namespace Simple.App.Integrations;

partial class IntegrationsFuncs
{
  public static IServiceCollection RegisterLogging(IServiceCollection services, ILoggerFactory loggerFactory) => services.AddSingleton(loggerFactory);

  public static IServiceCollection RegisterServices (IServiceCollection services) => services.AddProblemDetails();
}