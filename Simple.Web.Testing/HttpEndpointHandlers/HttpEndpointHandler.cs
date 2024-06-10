
using System.Net;
using System.Net.Http;
using System.Threading;

namespace Simple.Web.Testing;

public class HttpEndpointsHandler(IEnumerable<HttpEndpoint> endpoints) : HttpMessageHandler {

  readonly IEnumerable<HttpEndpoint> Endpoints = endpoints;

  protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default) =>
    GetHttpEndpoint(Endpoints, GetRequestPath(request)) switch {
      null => throw new WebException($"Endpoint with route '{GetRequestPath(request)}' not found."),
      var endpoint => Task.FromResult(endpoint.EndpointHandler(request))
    };
}