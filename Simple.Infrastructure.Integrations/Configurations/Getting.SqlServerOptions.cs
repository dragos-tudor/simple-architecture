
using Microsoft.Extensions.Configuration;

namespace Simple.Infrastructure.Integrations;

partial class IntegrationsFuncs
{
  public static SqlServerOptions GetSqlServerOptions (IConfiguration configuration) => GetConfigurationOptions<SqlServerOptions>(configuration);
}