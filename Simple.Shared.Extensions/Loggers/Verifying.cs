
using Microsoft.Extensions.Logging;

namespace Simple.Shared.Extensions;

partial class SharingFuncs
{
  public static bool IsErrorLogLevel(LogLevel level) => level == LogLevel.Error;
}