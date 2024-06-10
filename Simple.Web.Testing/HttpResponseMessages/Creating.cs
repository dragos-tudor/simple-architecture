

using System.Net;
using System.Net.Http;

namespace Simple.Web.Testing;

partial class TestingFuncs
{
  public static HttpResponseMessage CreateResponseMessage(HttpContent content, HttpStatusCode statusCode = HttpStatusCode.OK) =>
    new () {Content = content, StatusCode = statusCode};
}