
using System.Reflection;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static async Task<IEnumerable<string>> InitializeSqlServerAsync (SqlServerOptions options, CancellationToken cancellationToken = default)
  {
    var (adminName, adminPassword, userName, userPassword, imageName, containerName, dbName, networkName, serverPort) = options;

    using var dockerClient = CreateDockerClient();
    var container = await StartSqlServerAsync (dockerClient, adminPassword, imageName, containerName, serverPort, networkName, cancellationToken);

    var masterConnString = CreateSqlConnectionString("master", adminName, adminPassword, containerName);
    using var masterContext = CreateDbContext(CreateSqlContextOptions<DbContext>(masterConnString));
    await CreateSqlDatabaseAsync(masterContext, dbName, cancellationToken);
    await CreateSqlDatabaseUserAsync(masterContext, dbName, userName, userPassword, cancellationToken);

    return MigrateSqlDatabase(masterContext, dbName, Assembly.GetExecutingAssembly());
  }
}
