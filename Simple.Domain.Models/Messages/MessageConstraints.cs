
namespace Simple.Domain.Models;

public record MessageContraints
{
  public static int MessageTypeMaxLength => 150;
  public static bool MessageTypeRequired => true;

  public static bool MessageContentRequired => true;
  public static int TraceIdMaxLength => 24;
}