using Microsoft.Extensions.Logging;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  static readonly ILogger Logger = CreateLogger(GetLoggerFactory(), typeof(ApiFuncs).Namespace!);
}