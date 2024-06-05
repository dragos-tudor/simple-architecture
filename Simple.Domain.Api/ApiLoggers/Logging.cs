
using Microsoft.Extensions.Logging;

namespace Simple.Domain.Api;

partial class ApiFuncs
{
  [LoggerMessage(1, LogLevel.Information, "Contact: Contact {ContactId} created. [{TraceId}]", EventName = "LogContactCreated")]
  public static partial void LogContactCreated (ILogger logger, Guid contactId, string? traceId);

  [LoggerMessage(2, LogLevel.Information, "PhoneNumber: Phone number {Number} added to contact {ContactId}. [{TraceId}]", EventName = "LogPhoneNumberAdded")]
  public static partial void LogPhoneNumberAdded (ILogger logger, long number, Guid contactId, string? traceId);

  [LoggerMessage(3, LogLevel.Information, "Notification: Added to agenda notification sent from {From} to {To}. [{TraceId}]", EventName = "LogAddedToAgenda")]
  public static partial void LogAddedToAgendaNotification (ILogger logger, string from, string to, string? traceId);
}