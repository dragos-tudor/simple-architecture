
namespace Simple.Domain.Models;

public sealed record ContactValidationFailure (string Message, string? Source = default): ValidationFailure(Message, Source);

public sealed record ContactDuplicateFailure (string Message, string? Source = default): Failure(Message, Source);