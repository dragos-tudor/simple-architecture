
using Docker.DotNet.Models;

namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  static string GetServerIpAddress (EndpointSettings endpointSettings) => endpointSettings.IPAddress;
}