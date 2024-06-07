using Microsoft.Extensions.Logging;

namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  static readonly ILogger Logger = CreateLogger(GetLoggerFactory(), typeof(ServicesFuncs).Namespace!);
}