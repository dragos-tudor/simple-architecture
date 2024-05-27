
using FluentValidation;

namespace Simple.Shared.Models;

partial class ModelsFuncs
{
  public static IEnumerable<string> ValidateModel<TModel, TValidator> (TModel model, TValidator validator) where TValidator: AbstractValidator<TModel> =>
    validator.Validate(model).Errors.Select(ToStringValidationError);
}