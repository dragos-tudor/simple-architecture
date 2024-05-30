
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Docker.Extensions.DockerFuncs;
global using static Simple.Domain.Models.ModelsTests;

namespace Simple.Infrastructure.MongoDb;

[TestClass]
public partial class MongoDbTests
{
  [AssemblyInitialize]
  public static void InitializeMongeServer (TestContext _)
  {
    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(3));
    var cancellationToken = cancellationTokenSource.Token;
    var replicaSetOptions = new MongoReplicaSetOptions (
      "mongo:4.2.24", ["simple-mongo1", "simple-mongo2", "simple-mongo3"],
      "simple-network", "rs0", 27017,
      [ContactCollectionName, MessageCollectionName]
    );

    RunSynchronously(() => InitializeMongeReplicaSetAsync (replicaSetOptions, cancellationToken));

    // var mongoClient = CreateMongoClient(connectionString);
    // var database = GetMongoDatabase(mongoClient, DatabaseName);
    // CleanMongoCollections(database, [ContactCollectionName, MessageCollectionName]);
  }
}