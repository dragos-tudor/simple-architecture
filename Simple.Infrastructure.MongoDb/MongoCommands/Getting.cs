#pragma warning disable CA1307

namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  static string GetReplicaSetMember (int serverPort, string containerName, int index) =>
    $"{{_id:{index},host:\'{containerName}:{serverPort}\'}}";

  static IEnumerable<string> GetReplicaSetMembers (int serverPort, string[] containerNames) =>
    containerNames.Select((containerName, index) => GetReplicaSetMember(serverPort, containerName, index));

  public static string[] GetInitiateReplicaSetCommand (int serverPort, string replicaSet, string[] containerNames) => [
    "/bin/sh",
    "-c",
    "mongo --eval \"if(rs.status().ok != 1) { rs.initiate({_id:\'#replicaSet\', members:[#replicaMembers] }); exit; }\""
      .Replace("#replicaSet", replicaSet)
      .Replace("#replicaMembers", string.Join(",", GetReplicaSetMembers(serverPort, containerNames)))
  ];
}