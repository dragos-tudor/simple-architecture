
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static async Task<bool> InitializeMongoReplicaSetAsync (
    int serverPort,
    string masterId,
    string[] containerNames,
    string replicaSet,
    CancellationToken cancellationToken = default)
  {
    using var client = CreateDockerClient();
    await WaitForMongoReplicaSet(client.Exec, masterId, serverPort, replicaSet, containerNames, cancellationToken);
    return true;
  }
}