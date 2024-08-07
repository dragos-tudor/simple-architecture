
namespace Simple.Jobs;

partial class JobsFuncs
{
  internal static T GetRequiredService<T> (IServiceProvider services) where T: class => services.GetRequiredService<T>();
}