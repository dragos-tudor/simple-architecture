
namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  static readonly AddPhoneNumberRequestValidator AddPhoneNumberRequestValidator = new();

  static IEnumerable<string> ValidateAddPhoneNumberRequest(AddPhoneNumberRequest request) =>
    ValidateData(request, AddPhoneNumberRequestValidator);
}