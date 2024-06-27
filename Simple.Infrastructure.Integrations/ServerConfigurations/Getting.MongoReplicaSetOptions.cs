
using Microsoft.Extensions.Configuration;

namespace Simple.Infrastructure.Integrations;

partial class IntegrationsFuncs
{
  public static MongoReplicaSetOptions GetMongoReplicaSetOptions (IConfiguration configuration) => SanitizeReplicaSetOptions(GetConfigurationOptions<MongoReplicaSetOptions>(configuration));
}