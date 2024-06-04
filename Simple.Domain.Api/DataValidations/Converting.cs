
using FluentValidation.Results;

namespace Simple.Domain.Api;

partial class ApiFuncs
{
  static string ToStringValidationError (ValidationFailure failure) => $"${failure.PropertyName}: {failure.ErrorMessage}";
}