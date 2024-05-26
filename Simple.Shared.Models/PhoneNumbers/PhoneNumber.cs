
namespace Simple.Shared.Models;

public record PhoneNumber
{
  public required short CountryCode { get; set; } = 1;
  public required long Number { get; set; }
  public string? Extension { get; set; } = string.Empty;
  public PhoneNumberType NumberType { get; set; } = PhoneNumberType.Mobile;
}