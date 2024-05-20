
namespace Simple.Shared.Models;

public class Message
{
  public Guid MessageId { get; set; }
  public string MessageType { get; set; } = string.Empty;
  public string? TraceId { get; set; }
}

public class Message<TPayload>: Message
{
  public TPayload MessagePayload { get; set; } = default!;
}