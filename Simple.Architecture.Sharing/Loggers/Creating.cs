using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Simple.Architecture.Sharing;

partial class SharingFuncs
{
  public static ILogger CreateLogger(ILoggerFactory? loggerFactory, string loggerCategory) =>
    (loggerFactory ?? NullLoggerFactory.Instance).CreateLogger(loggerCategory);
}