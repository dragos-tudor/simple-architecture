
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;

namespace Simple.Web.Testing;

partial class TestingFuncs
{
  static IEnumerable<string> GetResponseMessageHeader (HttpResponseMessage response, string name) => response.Headers.GetValues(name);

  static string GetResponseMessageLocation (HttpResponseMessage response) => WebUtility.UrlDecode(response.Headers.Location!.OriginalString);

  static Task<string> ReadResponseMessageStringContent (HttpResponseMessage response) => response.Content.ReadAsStringAsync();

  static Task<T?> ReadResponseMessageJsonContent<T> (HttpResponseMessage response) => response.Content.ReadFromJsonAsync<T>();
}
