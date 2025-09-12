
namespace Simple.Worker;

partial class WorkerFuncs
{
  static IServiceCollection RegisterServices(IServiceCollection services) =>
    services.AddLogging().AddSingleton(TimeProvider.System).AddProblemDetails();
}