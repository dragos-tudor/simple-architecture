
namespace Simple.Shared.Models;

public record PhoneNumberContraints
{
  public static long MaxNumber => 99_999_999_999; // https://www.itu.int/rec/T-REC-E.164 [ITU-T E.164-number]
  public static bool NumberRequired => true;

  public static int CountryCodeMaxLength => 3;
  public static bool CountryCodeRequired => true;

  public static int ExtensionMaxLength => 5;
}