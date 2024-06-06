using Microsoft.Extensions.Logging;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static readonly ILogger Logger = CreateLogger(GetLoggerFactory(), typeof(ApiFuncs).Namespace!);
}