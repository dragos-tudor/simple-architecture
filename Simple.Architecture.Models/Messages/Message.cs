
namespace Simple.Architecture.Models;

public class Message
{
  public Guid MessageId { get; set; }
  public string MessageType { get; set; } = string.Empty;
  public string? TraceId { get; set; }
}