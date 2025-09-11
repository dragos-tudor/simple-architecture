global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using Simple.Infrastructure.SqlServer;
global using static Simple.Infrastructure.SqlServer.SqlServerTests;
global using static Simple.Testing.Models.ModelsFuncs;
global using static Storing.SqlServer.SqlServerFuncs;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Simple.Worker.Jobs;

[TestClass]
public partial class JobsTests
{
  static readonly string SqlConnectionString = CreateSqlConnectionString("agenda-jobs-tests", "dbuser", "P@ssw0rd!", "127.0.0.1");
  static readonly AgendaContextFactory SqlContextFactory = CreateAgendaContextFactory(SqlConnectionString);
  static readonly IMongoDatabase MongoDatabase = GetMongoDatabase("127.0.0.1", 27017, "agenda-jobs-tests");

  static void InitializeSqlDatabase<TContext>(TContext dbContext) where TContext : DbContext
  {
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
    dbContext.Database.Migrate();
  }

  [AssemblyInitialize]
  public static void InitializeDatabases(TestContext _)
  {
    string adminConnectionString = CreateSqlConnectionString("agenda-jobs-tests", "sa", "P@ssw0rd!", "127.0.0.1");
    using var dbContext = CreateAgendaContext(adminConnectionString);

    InitializeSqlDatabase(dbContext);
    CreateSqlDatabaseUser(dbContext, "agenda-jobs-tests", "dbuser", "P@ssw0rd!");
    MapModelClassTypes();
  }
}