
namespace Simple.Domain.Models;

public record PhoneNumberContraints
{
  public static long MaxNumber => 99_999_999_999; // https://www.itu.int/rec/T-REC-E.164 [ITU-T E.164-number]
  public static short MaxCountryCode => 999;
  public static int ExtensionMaxLength => 5;
}