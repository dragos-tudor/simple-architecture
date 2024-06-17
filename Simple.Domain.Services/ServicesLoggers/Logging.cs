
using Microsoft.Extensions.Logging;

namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  [LoggerMessage(1, LogLevel.Information, "Domain: contact {ContactId} created. [{CorrelationId}]", EventName = "LogContactCreated")]
  public static partial void LogContactCreated (ILogger logger, Guid contactId, string? correlationId);

  [LoggerMessage(2, LogLevel.Information, "Domain: phone number {Number} added to contact {ContactId}. [{CorrelationId}]", EventName = "LogPhoneNumberAdded")]
  public static partial void LogPhoneNumberAdded (ILogger logger, long number, Guid contactId, string? correlationId);

  [LoggerMessage(3, LogLevel.Information, "Domain: added to agenda notification sent from {From} to {To}. [{CorrelationId}]", EventName = "LogNotifiedAddedToAgenda")]
  public static partial void LogNotifiedAddedToAgenda (ILogger logger, string from, string to, string? correlationId);

  [LoggerMessage(4, LogLevel.Information, "Domain: phone number {Number} removed from contact {ContactId}. [{CorrelationId}]", EventName = "LogPhoneNumberRemoved")]
  public static partial void LogPhoneNumberRemoved (ILogger logger, long number, Guid contactId, string? correlationId);
}