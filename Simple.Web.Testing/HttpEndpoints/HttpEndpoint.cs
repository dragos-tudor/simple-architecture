
using System.Net.Http;

namespace Simple.Web.Testing;

public sealed record HttpEndpoint(string Route, Func<HttpRequestMessage, HttpResponseMessage> EndpointHandler);