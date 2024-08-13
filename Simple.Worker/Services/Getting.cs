
namespace Simple.Worker;

partial class WorkerFuncs
{
  internal static T GetRequiredService<T> (IServiceProvider services) where T: class => services.GetRequiredService<T>();
}