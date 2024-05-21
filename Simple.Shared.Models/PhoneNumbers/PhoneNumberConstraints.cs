
namespace Simple.Shared.Models;

public record PhoneNumberContraints
{
  public static int NumberLength => 9;
  public static bool NumberRequired => true;

  public static int CountryCodeMaxLength => 4;
  public static bool CountryCodeRequired => true;

  public static int ExtensionMaxLength => 5;
}