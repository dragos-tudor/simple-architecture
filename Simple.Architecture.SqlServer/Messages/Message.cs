
namespace Simple.Architecture.SqlServer;

public class MessageDb: Message
{
  public int MessageVersion { get; set; }
  public DateTime MessageDate { get; set; }
  public string MessageContent { get; set; } = string.Empty;
  public Guid? ParentId { get; set; }
  public bool IsActive { get; set; } = true;
}