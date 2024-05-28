
using FluentValidation;

namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static IEnumerable<string> ValidateModel<TModel, TValidator> (TModel model, TValidator validator) where TValidator: AbstractValidator<TModel> =>
    validator.Validate(model).Errors.Select(ToStringValidationError);

  public static IEnumerable<string> ValidateModels<TModel, TValidator> (IEnumerable<TModel> models, TValidator validator) where TValidator: AbstractValidator<TModel> =>
    models.SelectMany(model => ValidateModel(model, validator));
}