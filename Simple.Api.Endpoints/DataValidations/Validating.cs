
namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  static IEnumerable<string> ValidateData<T, TValidator>(T obj, TValidator validator) where TValidator : FluentValidation.AbstractValidator<T> where T : class =>
    validator.Validate(obj).Errors.Select(ToValidationError);

  static IEnumerable<string> ValidateData<T, TValidator>(IEnumerable<T> objects, TValidator validator) where TValidator : FluentValidation.AbstractValidator<T> where T : class =>
    objects.SelectMany(obj => ValidateData(obj, validator));
}