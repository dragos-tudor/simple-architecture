
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  static IEnumerable<ValidationFailure> ValidateData<T, TValidator> (T obj, TValidator validator) where TValidator: FluentValidation.AbstractValidator<T> where T: class =>
    validator.Validate(obj).Errors.Select(ToValidationFailure);

  static IEnumerable<ValidationFailure> ValidateData<T, TValidator> (IEnumerable<T> objects, TValidator validator) where TValidator: FluentValidation.AbstractValidator<T> where T: class =>
    objects.SelectMany(obj => ValidateData(obj, validator));
}