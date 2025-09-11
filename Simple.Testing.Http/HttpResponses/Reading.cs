
using System.Threading;

namespace Simple.Testing.Http;

partial class HttpFuncs
{
  static readonly JsonSerializerOptions WebJsonSerializerOptions = new(JsonSerializerDefaults.Web);

  public static ValueTask<T?> ReadHttpResponseJsonAsync<T>(HttpResponse httpResponse, JsonSerializerOptions? serializerOptions = default, CancellationToken cancellationToken = default) =>
    JsonSerializer.DeserializeAsync<T>(httpResponse.Body, serializerOptions ?? WebJsonSerializerOptions, cancellationToken);
}