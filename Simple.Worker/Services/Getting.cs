
namespace Simple.Worker;

partial class WorkerFuncs
{
  static T GetRequiredService<T>(IServiceProvider services) where T : class => services.GetRequiredService<T>();
}