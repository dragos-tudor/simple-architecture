
namespace Simple.Infrastructure.SqlServer;

public partial record SqlServerOptions(
  string AdminName,
  string AdminPassword,
  string UserName,
  string UserPassword,
  string ImageName,
  string ContainerName,
  string DbName,
  string NetworkName,
  int ServerPort
);

partial record SqlServerOptions
{
  public void Deconstruct(
    out string adminName,
    out string adminPassword,
    out string userName,
    out string userPassword,
    out string imageName,
    out string containerName,
    out string dbName,
    out string networkName,
    out int serverPort)
  {
    adminName = AdminName;
    adminPassword = AdminPassword;
    userName = UserName;
    userPassword = UserPassword;
    imageName = ImageName;
    containerName = ContainerName;
    dbName = DbName;
    networkName = NetworkName;
    serverPort = ServerPort;
  }
}