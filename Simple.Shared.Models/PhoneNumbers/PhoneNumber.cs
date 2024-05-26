
namespace Simple.Shared.Models;

public record PhoneNumber
{
  public required string CountryCode { get; set; } = "001";
  public required long Number { get; set; }
  public string? Extension { get; set; } = string.Empty;
  public PhoneNumberType NumberType { get; set; } = PhoneNumberType.Mobile;
}