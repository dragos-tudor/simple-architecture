
namespace Simple.Domain.Services;

using FluentValidation;

public sealed class PhoneNumberValidator : AbstractValidator<PhoneNumber>
{
  public PhoneNumberValidator()
  {
    RuleFor(phoneNumber => phoneNumber.CountryCode).NotEmpty();
    RuleFor(phoneNumber => phoneNumber.Number).NotEmpty();
    RuleFor(phoneNumber => phoneNumber.NumberType).IsInEnum();
  }
}