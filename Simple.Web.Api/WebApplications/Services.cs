
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static T GetRequiredService<T> (WebApplication app) where T: class => app.Services.GetRequiredService<T>();

  internal static IServiceCollection RegisterServices (WebApplicationBuilder builder) => builder.Services.AddProblemDetails();
}