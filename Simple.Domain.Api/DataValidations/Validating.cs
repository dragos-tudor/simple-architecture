
using FluentValidation;

namespace Simple.Domain.Api;

partial class ApiFuncs
{
  public static IEnumerable<string> ValidateObject<T, TValidator> (T obj, TValidator validator) where TValidator: AbstractValidator<T> where T: class =>
    validator.Validate(obj).Errors.Select(ToStringValidationError);

  public static IEnumerable<string> ValidateObjects<T, TValidator> (IEnumerable<T> objects, TValidator validator) where TValidator: AbstractValidator<T> where T: class =>
    objects.SelectMany(model => ValidateObject(model, validator));
}