
using Microsoft.Extensions.Logging;

namespace Simple.Shared.Extensions;

public class FakeLogger(Action<LogLevel, string> messageLogger): ILogger
{
  static readonly Action<LogLevel, string> DefaultMessageLogger = (_, message) => Console.WriteLine(message);
  readonly Action<LogLevel, string> MessageLogger = messageLogger ?? DefaultMessageLogger;

  public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default;

  public bool IsEnabled(LogLevel logLevel) => true;

  public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) =>
    MessageLogger(logLevel, formatter(state, exception));
}