
namespace Simple.App;

partial class AppFuncs
{
  public static T GetRequiredService<T> (IServiceProvider services) where T: class => services.GetRequiredService<T>();
}