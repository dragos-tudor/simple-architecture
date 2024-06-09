
using Microsoft.Extensions.Logging;

namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  [LoggerMessage(1, LogLevel.Information, "Domain: contact {ContactId} created. [{TraceId}]", EventName = "LogContactCreated")]
  public static partial void LogContactCreated (ILogger logger, Guid contactId, string? traceId);

  [LoggerMessage(2, LogLevel.Information, "Domain: phone number {Number} added to contact {ContactId}. [{TraceId}]", EventName = "LogPhoneNumberAdded")]
  public static partial void LogPhoneNumberAdded (ILogger logger, long number, Guid contactId, string? traceId);

  [LoggerMessage(3, LogLevel.Information, "Domain: added to agenda notification sent from {From} to {To}. [{TraceId}]", EventName = "LogNotifiedAddedToAgenda")]
  public static partial void LogNotifiedAddedToAgenda (ILogger logger, string from, string to, string? traceId);
}