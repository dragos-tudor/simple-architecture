
using Microsoft.Extensions.Configuration;

namespace Simple.App.Integrations;

partial class IntegrationsFuncs
{
  public static MongoReplicaSetOptions GetMongoReplicaSetOptions (IConfiguration configuration) => SanitizeReplicaSetOptions(GetConfigurationOptions<MongoReplicaSetOptions>(configuration));
}