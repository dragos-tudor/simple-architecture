
namespace Simple.Testing.Http;

partial class HttpFuncs
{
  static Task<string> ReadHttpResponseMessageStringAsync(HttpResponseMessage response) => response.Content.ReadAsStringAsync();

  public static Task<T?> ReadHttpResponseMessageJsonAsync<T>(HttpResponseMessage response) => response.Content.ReadFromJsonAsync<T>();
}
