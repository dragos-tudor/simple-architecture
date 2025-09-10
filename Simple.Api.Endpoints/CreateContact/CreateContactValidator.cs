
namespace Simple.Api.Endpoints;

public sealed class CreateContactRequestValidator : AbstractValidator<CreateContactRequest>
{
  public CreateContactRequestValidator()
  {
    RuleFor(contact => contact.ContactEmail).NotNull().MaximumLength(ContactConstraints.ContactEmailMaxLength);
    RuleFor(contact => contact.ContactName).NotNull().MaximumLength(ContactConstraints.ContactNameMaxLength);
  }
}