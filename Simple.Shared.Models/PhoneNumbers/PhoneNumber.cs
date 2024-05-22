
namespace Simple.Shared.Models;

public record PhoneNumber
{
  public required string CountryCode { get; set; } = "+1";
  public required string Number { get; set; } = string.Empty;
  public string? Extension { get; set; } = string.Empty;
  public PhoneNumberType NumberType { get; set; } = PhoneNumberType.Mobile;
}