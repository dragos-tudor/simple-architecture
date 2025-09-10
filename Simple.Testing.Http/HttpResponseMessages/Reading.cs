
namespace Simple.Testing.Http;

partial class HttpFuncs
{
  static Task<string> ReadHttpResponseMessageString(HttpResponseMessage response) => response.Content.ReadAsStringAsync();

  public static Task<T?> ReadHttpResponseMessageJson<T>(HttpResponseMessage response) => response.Content.ReadFromJsonAsync<T>();
}
