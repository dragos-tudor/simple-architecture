
namespace Simple.Web.Testing;

partial class TestingFuncs
{
  internal static HttpEndpoint? GetHttpEndpoint (IEnumerable<HttpEndpoint> endpoints, string route) =>
    endpoints.FirstOrDefault(endpoint => endpoint.Route == route);
}