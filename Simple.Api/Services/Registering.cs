
namespace Simple.Api;

partial class ApiFuncs
{
  static IServiceCollection RegisterServices(IServiceCollection services) =>
    services.AddLogging().AddProblemDetails();
}