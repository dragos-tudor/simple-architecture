using Microsoft.Extensions.Logging;

namespace Simple.Web.Endpoints;

partial class EndpointsFuncs
{
  static readonly ILogger Logger = CreateLogger(GetLoggerFactory(), typeof(EndpointsFuncs).Namespace!);
}