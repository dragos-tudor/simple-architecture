using SqlFuncs = Storing.SqlServer.SqlServerFuncs;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
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