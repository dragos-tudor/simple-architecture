
using System.Net.Http;
using System.Net.Http.Json;

namespace Simple.Web.Testing;

partial class TestingFuncs
{
  public static Task<string> ReadResponseMessageStringContent (HttpResponseMessage response) =>
    response.Content.ReadAsStringAsync();

  public static Task<T?> ReadResponseMessageJsonContent<T> (HttpResponseMessage response) =>
    response.Content.ReadFromJsonAsync<T>();
}
