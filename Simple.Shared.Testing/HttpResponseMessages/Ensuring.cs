
using System.Net;
using System.Net.Http;

namespace Simple.Shared.Testing;

partial class TestingFuncs
{
  public static async Task<bool> EnsureResponseMessageSuccess (HttpResponseMessage response)
  {
    if (response.StatusCode < (HttpStatusCode)400) return true;

    var problemDetails = await ReadResponseMessageStringContent(response);
    var errorMessage = GetResponseErrorMessage(response, problemDetails);

    throw new WebException(errorMessage);
  }
}
