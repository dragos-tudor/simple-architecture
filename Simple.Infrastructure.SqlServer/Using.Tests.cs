
global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using static Simple.Domain.Queries.QueriesFuncs;
global using static Simple.Testing.Models.ModelsFuncs;

namespace Simple.Infrastructure.SqlServer;

[TestClass]
public partial class SqlServerTests
{
  static readonly string SqlConnectionString = CreateSqlConnectionString("agenda-tests", "dbuser", "P@ssw0rd!", "127.0.0.1");

  static void InitializeSqlDatabase<TContext>(TContext dbContext) where TContext : DbContext
  {
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
    dbContext.Database.Migrate();
  }

  [AssemblyInitialize]
  public static void InitializeSqlServer(TestContext _)
  {
    string adminConnectionString = CreateSqlConnectionString("agenda-tests", "sa", "P@ssw0rd!", "127.0.0.1");
    using var agendaContext = CreateAgendaContext(adminConnectionString);

    InitializeSqlDatabase(agendaContext);
    CreateSqlDatabaseUser(agendaContext, "agenda-tests", "dbuser", "P@ssw0rd!");
  }
}