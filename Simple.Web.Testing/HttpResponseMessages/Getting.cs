
using System.Net;
using System.Net.Http;

namespace Simple.Web.Testing;

partial class TestingFuncs
{
  internal static IEnumerable<string> GetResponseMessageHeader (HttpResponseMessage response, string name) =>
    response.Headers.GetValues(name);

  internal static string GetResponseMessageLocation (HttpResponseMessage response) =>
    WebUtility.UrlDecode(response.Headers.Location!.OriginalString);
}
