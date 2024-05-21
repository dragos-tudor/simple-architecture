
using Docker.DotNet.Models;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
  static Action<CreateContainerParameters> SetCreateContainerParameters (int serverPort, string adminPassword) => (CreateContainerParameters @params) =>
  {
    @params.Env = ["ACCEPT_EULA=Y", $"SA_PASSWORD={adminPassword}"];
    @params.ExposedPorts = new Dictionary<string, EmptyStruct>() { { $"14330:{serverPort}", new EmptyStruct() } };
  };
}