using Microsoft.Extensions.Logging;

namespace Simple.Shared.Extensions;

partial class SharingFuncs
{
  public static ILoggerFactory? GetLoggerFactory(string? factoryKey = default) =>
    (ILoggerFactory?)AppContext.GetData(factoryKey ?? DefaultLoggerFactoryKey);
}