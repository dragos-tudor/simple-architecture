using Microsoft.Extensions.Logging;

namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  public static readonly ILogger Logger = CreateLogger(GetLoggerFactory(), typeof(MediatorFuncs).Namespace!);
}