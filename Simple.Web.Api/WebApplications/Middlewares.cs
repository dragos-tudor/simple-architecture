
using Microsoft.AspNetCore.Builder;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  static IApplicationBuilder UseMiddlewares (WebApplication app) => app.UseExceptionHandler().UseRouting();
}