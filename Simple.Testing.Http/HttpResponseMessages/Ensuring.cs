
namespace Simple.Testing.Http;

partial class HttpFuncs
{
  public static Task<bool> EnsureHttpResponseSuccessAsync(HttpResponseMessage response) =>
    EnsureHttpResponseStatusCodeAsync(response, HttpStatusCode.OK);

  public static async Task<bool> EnsureHttpResponseStatusCodeAsync(HttpResponseMessage response, HttpStatusCode statusCode)
  {
    if (response.StatusCode == statusCode) return true;
    if (response.StatusCode >= HttpStatusCode.BadRequest)
    {
      var problemDetails = await ReadHttpResponseMessageStringAsync(response);
      var errorMessage = GetHttpResponseMessageError(response, problemDetails);

      throw new WebException(errorMessage);
    }
    throw new WebException($"Http response status code {response.StatusCode}.");
  }
}
