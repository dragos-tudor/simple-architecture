using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Simple.Shared.Extensions;

partial class ExtensionsFuncs
{
  public static ILogger CreateLogger(ILoggerFactory? loggerFactory, string loggerCategory) =>
    (loggerFactory ?? NullLoggerFactory.Instance).CreateLogger(loggerCategory);
}