
namespace Simple.Testing.Http;

partial class HttpFuncs
{
  public static async Task<bool> EnsureHttpResponseMessageSuccess(HttpResponseMessage response)
  {
    if (response.StatusCode < (HttpStatusCode)400) return true;

    var problemDetails = await ReadHttpResponseMessageString(response);
    var errorMessage = GetHttpResponseMessageError(response, problemDetails);

    throw new WebException(errorMessage);
  }
}
