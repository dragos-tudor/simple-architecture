
using System.Net;
using System.Net.Http;

namespace Simple.Shared.Testing;

partial class TestingFuncs
{
  static string GetResponseErrorMessage (HttpResponseMessage response, string problemDetails) => response.ToString() + Environment.NewLine + problemDetails;

  public static string GetResponseMessageLocation (HttpResponseMessage response) => WebUtility.UrlDecode(response.Headers.Location!.OriginalString);
}
