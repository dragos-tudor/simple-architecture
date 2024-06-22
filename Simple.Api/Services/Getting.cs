
namespace Simple.Api;

partial class ApiFuncs
{
  public static T GetRequiredService<T> (IServiceProvider services) where T: class => services.GetRequiredService<T>();
}