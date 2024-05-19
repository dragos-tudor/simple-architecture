

namespace Simple.Architecture.Models;

public class PhoneNumber {
  public string Number { get; set; } = string.Empty;
  public string CountryCode { get; set; } = "+1";
  public string? Extension { get; set; } = string.Empty;
  public PhoneNumberType NumberType { get; set; } = PhoneNumberType.Mobile;
}