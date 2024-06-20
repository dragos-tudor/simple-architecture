
namespace Simple.Infrastructure.SqlServer;

public partial record SqlServerOptions
{
  public string AdminName { get; init; } = string.Empty;
  public string AdminPassword { get; init; } = string.Empty;
  public string UserName { get; init; } = string.Empty;
  public string UserPassword { get; init; } = string.Empty;
  public string ImageName { get; init; } = "mcr.microsoft.com/mssql/server:2019-latest";
  public string ContainerName { get; init; } = "simple-sql";
  public string DbName { get; init; } = "agenda";
  public string NetworkName { get; init; } = "simple-network";
  public int ServerPort { get; init; } = 1433;
}

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