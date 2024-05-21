
namespace Simple.Shared.Models;

public class Message
{
  public Guid MessageId { get; set; }
  public string MessageType { get; set; } = string.Empty;
  public int MessageVersion { get; set; }
  public DateTime MessageDate { get; set; }
  public string MessageContent { get; set; } = string.Empty;
  public Guid? ParentId { get; set; }
  public string? TraceId { get; set; }
  public bool IsActive { get; set; } = true;

  public override string ToString() => $"{MessageType} - {MessageId}";
}

public class Message<TPayload>: Message
{
  public TPayload MessagePayload { get; set; } = default!;
}