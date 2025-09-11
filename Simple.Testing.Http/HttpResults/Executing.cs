
using Microsoft.Extensions.DependencyInjection;

namespace Simple.Testing.Http;

partial class HttpFuncs
{
  public static async Task<HttpContext> ExecuteHttpResultAsync(IResult result, HttpContext httpContext)
  {
    await result.ExecuteAsync(httpContext);
    httpContext.Response.Body.Position = 0;
    return httpContext;
  }
}