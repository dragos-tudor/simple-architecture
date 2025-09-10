
namespace Simple.Testing.Http;

partial class HttpFuncs
{
  public static HttpContext CreateHttpContext(string contentType = "application/json")
  {
    var httpContext = new DefaultHttpContext();
    httpContext.Response.ContentType = contentType;
    return httpContext;
  }
}