
namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  static string ToValidationError(ValidationFailure failure) => $"${failure.PropertyName}: {failure.ErrorMessage}";
}