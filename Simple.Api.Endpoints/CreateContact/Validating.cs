
namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  static readonly CreateContactRequestValidator CreateContactRequestValidator = new();

  static IEnumerable<string> ValidateCreateContactRequest(CreateContactRequest request) =>
    ValidateData(request, CreateContactRequestValidator);
}