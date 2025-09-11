
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Simple.Domain.Queries.QueriesFuncs;
global using static Simple.Testing.Models.ModelsFuncs;

namespace Simple.Infrastructure.MongoDb;

[TestClass]
public partial class MongoDbTests
{
  static readonly IMongoDatabase MongoDatabase = GetMongoDatabase("127.0.0.1", 27017, "agenda-tests");

  [AssemblyInitialize]
  public static void InitializeTests(TestContext _) =>
    InitializeMongoDatabase();
}