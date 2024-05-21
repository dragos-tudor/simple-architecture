
namespace Simple.Shared.Models;

public record ContactConstraints
{
  public static int ContactEmailMaxLength => 50;
  public static bool ContactEmailRequired => true;

  public static int ContactNameMaxLength => 50;
  public static bool ContactNameRequired => true;
}