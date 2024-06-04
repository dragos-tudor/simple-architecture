
using Microsoft.Extensions.Logging;

namespace Simple.Domain.Api;

partial class ApiFuncs
{
  [LoggerMessage(1, LogLevel.Information, "Contact: Contact created {ContactId}. [{TraceId}]", EventName = "LogContactCreated")]
  public static partial void LogContactCreated (ILogger logger, Guid contactId, string? traceId);

  [LoggerMessage(2, LogLevel.Information, "PhoneNumber: Add phone number {Number} to contact {ContactId}. [{TraceId}]", EventName = "LogPhoneNumberAdded")]
  public static partial void LogPhoneNumberAdded (ILogger logger, long number, Guid contactId, string? traceId);
}