

namespace Simple.Web.Testing;

partial class TestingFuncs
{
  static HttpEndpointsHandler CreateHttpEndpointsHandler (HttpEndpoint[] endpoints) =>
    new (endpoints);
}