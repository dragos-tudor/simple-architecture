
using FluentValidation.Results;

namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  static string ToStringValidationError (ValidationFailure failure) => $"${failure.PropertyName}: {failure.ErrorMessage}";
}