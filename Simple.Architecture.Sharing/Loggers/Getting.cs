using Microsoft.Extensions.Logging;

namespace Simple.Architecture.Sharing;

partial class SharingFuncs
{
  public static ILoggerFactory? GetLoggerFactory(string? factoryKey = default) =>
    (ILoggerFactory?)AppContext.GetData(factoryKey ?? DefaultLoggerFactoryKey);
}