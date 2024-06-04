using Microsoft.Extensions.Logging;

namespace Simple.Domain.Api;

partial class ApiFuncs
{
  static readonly ILogger Logger = CreateLogger(GetLoggerFactory(), typeof(ApiFuncs).Namespace!);
}