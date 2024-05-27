
namespace Simple.Domain.Models;

public record Message
{
  public Guid MessageId { get; set; }
  public required string MessageType { get; set; } = string.Empty;
  public int MessageVersion { get; set; }
  public DateTime MessageDate { get; set; }
  public required string MessageContent { get; set; } = string.Empty;
  public Guid? ParentId { get; set; }
  public string? TraceId { get; set; }
  public bool IsActive { get; set; } = true;

  public override string ToString() => MessageType;
}

public record Message<TPayload>: Message
{
  public TPayload MessagePayload { get; set; } = default!;
}