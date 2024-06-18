
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  static string[] GetDistinctCollNames (MongoReplicaSetOptions replicaSetOptions) => replicaSetOptions.CollNames.Distinct().ToArray();

  static string[] GetDistinctContainerNames (MongoReplicaSetOptions replicaSetOptions) => replicaSetOptions.ContainerNames.Distinct().ToArray();

  public static MongoReplicaSetOptions SanitizeReplicaSetOptions (MongoReplicaSetOptions replicaSetOptions) =>
    replicaSetOptions with { CollNames = GetDistinctCollNames(replicaSetOptions), ContainerNames = GetDistinctContainerNames(replicaSetOptions) };
}