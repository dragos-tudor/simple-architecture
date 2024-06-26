
namespace Simple.App;

partial class AppFuncs
{
  internal static T GetRequiredService<T> (IServiceProvider services) where T: class => services.GetRequiredService<T>();
}