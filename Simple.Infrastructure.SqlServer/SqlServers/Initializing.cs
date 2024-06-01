
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static async Task<IEnumerable<string>> InitializeSqlServerAsync (SqlServerOptions options, CancellationToken cancellationToken = default)
  {
    var (adminName, adminPassword, userName, userPassword, imageName, containerName, databaseName, serverPort, networkName) = options;

    using var dockerClient = CreateDockerClient(new Uri("http://172.17.0.1:2375"));
    var container = await StartSqlServerAsync (dockerClient, adminPassword, imageName, containerName, serverPort, networkName, cancellationToken);
    SetAgendaContextFactory(CreateDbContextFactory(CreateAgendaContextOptions(containerName, databaseName, userName, userPassword)));

    using var masterContext = CreateMasterContext(containerName, adminName, adminPassword);
    await CreateSqlDatabaseAsync(masterContext, databaseName, cancellationToken);
    await CreateSqlDatabaseUserAsync(masterContext, databaseName, userName, userPassword, cancellationToken);

    return MigrateSqlDatabase(masterContext, databaseName);
  }
}
