using Docker.DotNet.Models;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
  static string GetServerIpAddress (NetworkSettings network) => network.IPAddress;
}