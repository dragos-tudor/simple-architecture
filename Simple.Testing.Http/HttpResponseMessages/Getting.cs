
namespace Simple.Testing.Http;

partial class HttpFuncs
{
  static string GetHttpResponseMessageError(HttpResponseMessage response, string problemDetails) => response.ToString() + Environment.NewLine + problemDetails;

  public static string GetHttpResponseMessageLocation(HttpResponseMessage response) => WebUtility.UrlDecode(response.Headers.Location!.OriginalString);
}
