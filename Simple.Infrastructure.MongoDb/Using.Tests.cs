
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Simple.Shared.Testing.TestingFuncs;

namespace Simple.Infrastructure.MongoDb;

[TestClass]
public partial class MongoDbTests
{
  static readonly MongoReplicaSetOptions MongoReplicaSetOptions = new MongoReplicaSetOptions() { ContainerNames = ["simple-mongo1", "simple-mongo2", "simple-mongo3"], CollNames = ["contacts", "messages"], DbName = "agenda-tests" };
  static readonly IMongoDatabase MongoDatabase = GetMongoDatabase(MongoReplicaSetOptions);

  [AssemblyInitialize]
  public static void InitializeMongeServer (TestContext _)
  {
    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(3));
    var cancellationToken = cancellationTokenSource.Token;

    RunSynchronously(() =>
      InitializeMongeReplicaSetAsync (MongoReplicaSetOptions, cancellationToken));
  }
}