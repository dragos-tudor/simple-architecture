
using System.IO;
using Microsoft.Extensions.DependencyInjection;

namespace Simple.Testing.Http;

partial class HttpFuncs
{
  public static HttpContext CreateHttpContext() =>
    new DefaultHttpContext
    {
      RequestServices = new ServiceCollection().AddLogging().BuildServiceProvider(),
      Response = { Body = new MemoryStream() }
    };
}