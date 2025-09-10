
namespace Simple.Api.Endpoints;

public partial record AddPhoneNumberRequest
{
  public required short CountryCode { get; set; } = 1;
  public required long Number { get; set; }
  public short? Extension { get; set; }
  public PhoneNumberType NumberType { get; set; } = PhoneNumberType.Mobile;
}