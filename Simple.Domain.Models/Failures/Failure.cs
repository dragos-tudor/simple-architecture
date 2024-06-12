
namespace Simple.Domain.Models;

public record Failure (string Message, string? Source = default);

public record ValidationFailure (string Message, string? Source = default): Failure(Message, Source);