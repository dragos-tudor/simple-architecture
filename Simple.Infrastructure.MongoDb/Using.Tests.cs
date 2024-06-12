
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Simple.Shared.Testing.TestingFuncs;

namespace Simple.Infrastructure.MongoDb;

[TestClass]
public partial class MongoDbTests
{
  static readonly IMongoDatabase Database = GetMongoDatabase(CreateMongoClient(GetMongoConnectionString("simple-mongo1,simple-mongo2,simple-mongo3", "rs0")), "agenda-tests");

  [AssemblyInitialize]
  public static void InitializeMongeServer (TestContext _)
  {
    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(3));
    var cancellationToken = cancellationTokenSource.Token;
    var replicaSetOptions = new MongoReplicaSetOptions (
      "mongo:4.2.24", ["simple-mongo1", "simple-mongo2", "simple-mongo3"],
      "simple-network", "rs0",
      "agenda-tests", ["contacts", "messages"],
      27017
    );

    RunSynchronously(() =>
      InitializeMongeReplicaSetAsync (replicaSetOptions, cancellationToken));
  }
}