
namespace Simple.Testing.Http;

partial class HttpFuncs
{
  public static async Task<bool> EnsureHttpResponseMessageSuccessAsync(HttpResponseMessage response)
  {
    if (response.StatusCode < (HttpStatusCode)400) return true;

    var problemDetails = await ReadHttpResponseMessageStringAsync(response);
    var errorMessage = GetHttpResponseMessageError(response, problemDetails);

    throw new WebException(errorMessage);
  }
}
