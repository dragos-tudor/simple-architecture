
using Microsoft.Extensions.DependencyInjection;

namespace Simple.App.Integrations;

partial class IntegrationsFuncs
{
  public static T GetRequiredService<T> (IServiceProvider services) where T: class => services.GetRequiredService<T>();
}