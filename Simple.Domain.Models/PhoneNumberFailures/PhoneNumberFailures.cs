
namespace Simple.Domain.Models;

public sealed record PhoneNumberValidationFailure (string Message, string? Source = default): ValidationFailure(Message, Source);

public sealed record PhoneNumberDuplicateFailure (string Message, string? Source = default): Failure(Message, Source);