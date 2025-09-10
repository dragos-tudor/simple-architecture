
namespace Simple.Testing.Http;

partial class HttpFuncs
{
  public static async Task<HttpContext> ExecuteResultAsync(IResult result, HttpContext? httpContext = default)
  {
    httpContext ??= CreateHttpContext();
    await result.ExecuteAsync(httpContext);
    return httpContext;
  }
}