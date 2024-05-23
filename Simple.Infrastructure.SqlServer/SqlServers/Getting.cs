using Docker.DotNet.Models;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  static string GetServerIpAddress (NetworkSettings network) => network.IPAddress;
}