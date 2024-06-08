
namespace Simple.Domain.Services;

using FluentValidation;

public sealed class ContactValidator : AbstractValidator<Contact>
{
  public ContactValidator()
  {
    RuleFor(contact => contact.ContactEmail).NotNull().MaximumLength(ContactConstraints.ContactEmailMaxLength);
    RuleFor(contact => contact.ContactName).NotNull().MaximumLength(ContactConstraints.ContactNameMaxLength);
    // RuleForEach(contact => contact.PhoneNumbers ?? []).SetValidator(new PhoneNumberValidator());
  }
}