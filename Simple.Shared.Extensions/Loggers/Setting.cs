using Microsoft.Extensions.Logging;

namespace Simple.Shared.Extensions;

partial class ExtensionsFuncs
{
  const string DefaultLoggerFactoryKey = nameof(ILoggerFactory);

  public static void SetLoggerFactory(ILoggerFactory loggerFactory, string? factoryKey = default) =>
    AppContext.SetData(factoryKey ?? DefaultLoggerFactoryKey, loggerFactory);
}