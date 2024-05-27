namespace Simple.Web.Endpoints;

using FluentValidation;

sealed class ContactValidator : AbstractValidator<Contact>
{
  public ContactValidator()
  {
    RuleFor(contact => contact.ContactId).NotEmpty();
    RuleFor(contact => contact.ContactEmail).NotNull().MaximumLength(ContactConstraints.ContactEmailMaxLength);
    RuleFor(contact => contact.ContactName).NotNull().MaximumLength(ContactConstraints.ContactNameMaxLength);
    RuleForEach(contact => contact.PhoneNumbers).SetValidator(new PhoneNumberValidator());
  }
}

partial class EndpointsFuncs
{
  static readonly ContactValidator ContactValidator =  new ();
}