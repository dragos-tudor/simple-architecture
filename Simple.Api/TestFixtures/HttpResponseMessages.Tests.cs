#pragma warning disable CA2201

using System.Net;
using System.Net.Http;

namespace Simple.Api;

partial class ApiTesting
{
  static IEnumerable<string> GetResponseMessageHeader (HttpResponseMessage response, string name) => response.Headers.GetValues(name);

  static string GetResponseMessageLocation (HttpResponseMessage response) => WebUtility.UrlDecode(response.Headers.Location!.OriginalString);

  static string GetResponseErrorMessage(HttpResponseMessage response, string problemDetails) => response.ToString() + Environment.NewLine + problemDetails;

  static async Task<bool> EnsureResponseMessageSuccess (HttpResponseMessage response)
  {
    if (response.StatusCode < (HttpStatusCode)400) return true;
    var problemDetails = await ReadResponseMessageStringContent(response);
    var errorMessage = GetResponseErrorMessage(response, problemDetails);
    throw new Exception(errorMessage);
  }

  static Task<string> ReadResponseMessageStringContent (HttpResponseMessage response) => response.Content.ReadAsStringAsync();

  static Task<T?> ReadResponseMessageJsonContent<T> (HttpResponseMessage response) => response.Content.ReadFromJsonAsync<T>();
}
