
namespace Simple.Shared.Models;

public sealed record MessageIdempotency(Guid ParentId, string MessageType);