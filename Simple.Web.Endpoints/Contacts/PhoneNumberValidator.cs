namespace Simple.Web.Endpoints;

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
