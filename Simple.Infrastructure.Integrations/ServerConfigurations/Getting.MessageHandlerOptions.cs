
using Microsoft.Extensions.Configuration;

namespace Simple.Infrastructure.Integrations;

partial class IntegrationsFuncs
{
  public static MessageHandlerOptions GetMessageHandlerOptions (IConfiguration configuration) => GetConfigurationOptions<MessageHandlerOptions>(configuration);
}