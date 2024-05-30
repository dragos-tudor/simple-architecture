#pragma warning disable CA1819

namespace Simple.Infrastructure.MongoDb;

public partial record MongoReplicaSetOptions(
  string ImageName,
  string[] ContainerNames,
  string NetworkName,
  string ReplicaSet,
  int ServerPort,
  string[] CollNames
);

partial record MongoReplicaSetOptions
{
  public void Deconstruct(
    out string imageName,
    out string[] containerNames,
    out string networkName,
    out string replicaSet,
    out int serverPort,
    out string[] collNames)
  {
    imageName = ImageName;
    containerNames = ContainerNames;
    networkName = NetworkName;
    replicaSet = ReplicaSet;
    serverPort = ServerPort;
    collNames = CollNames;
  }
}