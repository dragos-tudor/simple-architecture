
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Docker.Extensions.DockerFuncs;
global using static Simple.Domain.Models.ModelsTests;

namespace Simple.Infrastructure.MongoDb;

[TestClass]
public partial class MongoDbTests
{
  const string MongoDatabaseName = "agenda";

  [AssemblyInitialize]
  public static void InitializeMongeServer(TestContext _)
  {
    const string imageName = "mongo:4.2.24";
    const string networkName = "simple-network";
    const string replicaSet = "rs0";
    const int serverPort = 27017;

    using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(3));
    MongoDbClient = RunSynchronously(() => StartMongoServer(imageName, ["simple-mongo1", "simple-mongo2"], serverPort, networkName, replicaSet, cts.Token));
    var database = GetMongoDatabase(MongoDatabaseName, MongoDbClient);

    if(!ExistsMongoCollections(database)) CreateMongoCollections(database, ["contacts", "messages"]);
    CleanMongoCollections(database, ["contacts", "messages"]);

    MapModelClassTypes();
  }
}