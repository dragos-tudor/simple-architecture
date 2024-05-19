using Microsoft.Extensions.Logging;

namespace Simple.Architecture.Mediator;

partial class MediatorFuncs
{
  public static readonly ILogger Logger = CreateLogger(GetLoggerFactory(), typeof(MediatorFuncs).Namespace!);
}