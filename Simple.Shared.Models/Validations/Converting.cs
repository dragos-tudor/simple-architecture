
using FluentValidation.Results;

namespace Simple.Shared.Models;

partial class ModelsFuncs
{
  static string ToStringValidationError (ValidationFailure failure) => $"${failure.PropertyName}: {failure.ErrorMessage}";
}