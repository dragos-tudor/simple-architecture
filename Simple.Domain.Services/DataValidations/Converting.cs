
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  static ValidationFailure ToValidationFailure (FluentValidation.Results.ValidationFailure failure) => new ($"${failure.PropertyName}: {failure.ErrorMessage}");
}