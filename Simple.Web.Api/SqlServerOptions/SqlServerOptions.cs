
namespace Simple.Web.Api;

public partial record SqlServerOptions(
  string AdminName,
  string AdminPassword,
  string UserName,
  string UserPassword,
  string ImageName,
  string ContainerName,
  string DatabaseName,
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
    out string databaseName,
    out int serverPort)
  {
    adminName = AdminName;
    adminPassword = AdminPassword;
    userName = UserName;
    userPassword = UserPassword;
    imageName = ImageName;
    containerName = ContainerName;
    databaseName = DatabaseName;
    serverPort = ServerPort;
  }
}