
using Docker.DotNet.Models;

namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  static EndpointSettings GetEndpointSettings (NetworkSettings networkSettings, string networkName) =>
    networkSettings.Networks[networkName];

  static NetworkResponse? GetNamedNetwork (IEnumerable<NetworkResponse> networkResponses, string networkName) =>
    networkResponses.FirstOrDefault(networkResponse => networkResponse.Name == networkName);
}