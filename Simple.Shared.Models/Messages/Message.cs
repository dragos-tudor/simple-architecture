
namespace Simple.Shared.Models;

public record Message
{
  public Guid MessageId { get; set; }
  public required string MessageType { get; set; } = string.Empty;
  public int MessageVersion { get; set; }
  public DateTime MessageDate { get; set; }
  public required string MessageContent { get; set; } = string.Empty;

  public Guid? ParentId { get; set; }
  public string? CorrelationId { get; set; }

  public string ErrorMessage { get; set; } = string.Empty;
  public byte? ErrorCounter { get; set; }
  public bool IsPending { get; set; } = true;
}

public record Message<TPayload> : Message
{
  public TPayload MessagePayload { get; set; } = default!;
}