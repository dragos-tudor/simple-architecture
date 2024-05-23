
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static string StartSqlServer (int serverPort, string adminPassword, string imageName, string containerName)
  {
    var networkSettings = StartSqlContainer(serverPort, adminPassword, imageName, containerName);
    return GetServerIpAddress(networkSettings);
  }
}