
namespace Simple.Domain.Models;

public sealed record MessageIdempotency (Guid ParentId, string MessageType);