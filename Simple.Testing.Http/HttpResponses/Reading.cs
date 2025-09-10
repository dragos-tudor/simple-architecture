
namespace Simple.Testing.Http;

partial class HttpFuncs
{
  public static T ReadHttpResponseJson<T>(HttpResponse httpResponse) => JsonSerializer.Deserialize<T>(httpResponse.Body)!;
}