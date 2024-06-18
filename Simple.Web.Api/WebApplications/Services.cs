
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  static T GetRequiredService<T> (WebApplication app) where T: class => app.Services.GetRequiredService<T>();

  static IServiceCollection RegisterLogging(WebApplicationBuilder builder, ILoggerFactory loggerFactory) => builder.Services.AddSingleton<ILoggerFactory>(loggerFactory);

  static IServiceCollection RegisterServices (WebApplicationBuilder builder) => builder.Services.AddProblemDetails();
}