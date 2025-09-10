
namespace Simple.Api.Endpoints;

public sealed class AddPhoneNumberRequestValidator : AbstractValidator<AddPhoneNumberRequest>
{
  public AddPhoneNumberRequestValidator()
  {
    RuleFor(request => request.CountryCode).NotEmpty();
    RuleFor(request => request.Number).NotEmpty();
    RuleFor(request => request.NumberType).IsInEnum();
  }
}