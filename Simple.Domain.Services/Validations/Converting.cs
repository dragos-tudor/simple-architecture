
using System.ComponentModel.DataAnnotations;
using FluentValidation.Results;

namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  static ValidationException ToValidationException (ValidationFailure failure) => new ($"${failure.PropertyName}: {failure.ErrorMessage}");
}