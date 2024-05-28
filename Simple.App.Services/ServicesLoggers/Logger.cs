using Microsoft.Extensions.Logging;

namespace Simple.App.Services;

partial class ServicesFuncs
{
  static readonly ILogger Logger = CreateLogger(GetLoggerFactory(), typeof(ServicesFuncs).Namespace!);
}