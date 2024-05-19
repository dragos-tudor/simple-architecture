using Microsoft.Extensions.Logging;

namespace Simple.Architecture.Queue;

partial class QueueFuncs
{
  static readonly ILogger Logger = CreateLogger(GetLoggerFactory(), typeof(QueueFuncs).Namespace!);
}