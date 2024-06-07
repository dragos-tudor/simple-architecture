
using System.ComponentModel.DataAnnotations;

namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  static IEnumerable<ValidationException> ValidateData<T, TValidator> (T obj, TValidator validator) where TValidator: FluentValidation.AbstractValidator<T> where T: class =>
    validator.Validate(obj).Errors.Select(ToValidationException);

  static IEnumerable<ValidationException> ValidateData<T, TValidator> (IEnumerable<T> objects, TValidator validator) where TValidator: FluentValidation.AbstractValidator<T> where T: class =>
    objects.SelectMany(obj => ValidateData(obj, validator));
}