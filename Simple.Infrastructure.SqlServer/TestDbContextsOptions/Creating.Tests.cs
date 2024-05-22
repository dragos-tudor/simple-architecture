using SqlFuncs = Storing.SqlServer.SqlServerFuncs;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
  const string AdminName = "sa";
  const string AdminPassword = "admin.P@ssw0rd";

  static DbContextOptions<TContext> CreateDbContextOptions<TContext> (string dbName) where TContext: DbContext =>
    IsInMemoryContext()?
      SqlFuncs.CreateInMemoryContextOptions<TContext>(dbName):
      SqlFuncs.CreateSqlContextOptions<TContext>(
        CreateDbConnectionString(
          dbName,
          ServerIpAddress,
          AdminName,
          AdminPassword));
}