
using System.Net;
using System.Net.Http;

namespace Simple.Web.Testing;

partial class TestingFuncs
{
  static IEnumerable<string> GetResponseMessageHeader (HttpResponseMessage response, string name) =>
    response.Headers.GetValues(name);

  public static string GetResponseMessageLocation (HttpResponseMessage response) =>
    WebUtility.UrlDecode(response.Headers.Location!.OriginalString);
}
