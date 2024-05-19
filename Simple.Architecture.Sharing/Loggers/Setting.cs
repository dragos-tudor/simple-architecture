using Microsoft.Extensions.Logging;

namespace Simple.Architecture.Sharing;

partial class SharingFuncs
{
  const string DefaultLoggerFactoryKey = nameof(ILoggerFactory);

  public static void SetLoggerFactory(ILoggerFactory loggerFactory, string? factoryKey = default) =>
    AppContext.SetData(factoryKey ?? DefaultLoggerFactoryKey, loggerFactory);
}