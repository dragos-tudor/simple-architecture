#pragma warning disable CA1819

namespace Simple.Infrastructure.MongoDb;

public partial record MongoReplicaSetOptions
{
  public string ImageName { get; init; } = "mongo:4.2.24";
  public string[] ContainerNames { get; init; } = [];
  public string NetworkName { get; init; } = "simple-network";
  public string ReplicaSet { get; init; } = "rs0";
  public string DbName { get; init; } =  "agenda";
  public string[] CollNames { get; init; } = [];
  public int ServerPort { get; init; } = 27017;
}

partial record MongoReplicaSetOptions
{
  public void Deconstruct(
    out string imageName,
    out string[] containerNames,
    out string networkName,
    out string replicaSet,
    out string dbName,
    out string[] collNames,
    out int serverPort)
  {
    imageName = ImageName;
    containerNames = ContainerNames;
    networkName = NetworkName;
    replicaSet = ReplicaSet;
    dbName = DbName;
    collNames = CollNames;
    serverPort = ServerPort;
  }
}