using Microsoft.Extensions.Logging;

namespace Simple.Infrastructure.Queue;

partial class QueueFuncs
{
  internal static readonly ILogger Logger = CreateLogger(GetLoggerFactory(), typeof(QueueFuncs).Namespace!);
}