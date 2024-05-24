
using Docker.DotNet.Models;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  static Action<CreateContainerParameters> SetCreateContainerParameters (int serverPort, string adminPassword) => (CreateContainerParameters @params) =>
  {
    @params.Env = ["ACCEPT_EULA=Y", $"SA_PASSWORD={adminPassword}"];
    @params.ExposedPorts = new Dictionary<string, EmptyStruct>() { { $"{serverPort}:{serverPort}", new EmptyStruct() } };
  };
}