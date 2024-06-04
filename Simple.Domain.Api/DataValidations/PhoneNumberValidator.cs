
namespace Simple.Domain.Api;

using FluentValidation;

sealed class PhoneNumberValidator : AbstractValidator<PhoneNumber>
{
  public PhoneNumberValidator()
  {
    RuleFor(phoneNumber => phoneNumber.CountryCode).NotEmpty();
    RuleFor(phoneNumber => phoneNumber.Number).NotEmpty();
    RuleFor(phoneNumber => phoneNumber.Extension).NotNull().MaximumLength(PhoneNumberContraints.ExtensionMaxLength);
  }
}

partial class ApiFuncs
{
  static readonly PhoneNumberValidator PhoneNumberValidator =  new ();
}