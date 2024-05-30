
using Docker.DotNet.Models;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  static Action<CreateContainerParameters> SetSqlCreateContainerParameters (string adminPassword, string containerName, string networkName) => (CreateContainerParameters @params) =>
  {
    @params.Hostname = containerName;
    @params.HostConfig = new HostConfig() { NetworkMode = networkName };
    @params.Env = ["ACCEPT_EULA=Y", $"SA_PASSWORD={adminPassword}"];
  };
}