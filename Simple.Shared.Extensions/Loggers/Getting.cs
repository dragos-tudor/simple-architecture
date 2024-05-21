using Microsoft.Extensions.Logging;

namespace Simple.Shared.Extensions;

partial class ExtensionsFuncs
{
  public static ILoggerFactory? GetLoggerFactory(string? factoryKey = default) =>
    (ILoggerFactory?)AppContext.GetData(factoryKey ?? DefaultLoggerFactoryKey);
}