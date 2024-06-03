#pragma warning disable CA1819

namespace Simple.Infrastructure.MongoDb;

public partial record MongoReplicaSetOptions(
  string ImageName,
  string[] ContainerNames,
  string NetworkName,
  string ReplicaSet,
  string DbName,
  string[] CollNames,
  int ServerPort
);

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