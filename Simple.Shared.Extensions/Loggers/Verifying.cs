
using Microsoft.Extensions.Logging;

namespace Simple.Shared.Extensions;

partial class ExtensionsFuncs
{
  public static bool IsErrorLogLevel(LogLevel level) => level == LogLevel.Error;
}