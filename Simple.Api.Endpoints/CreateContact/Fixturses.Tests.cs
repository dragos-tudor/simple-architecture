
namespace Simple.Api.Endpoints;

partial class EndpointsTests
{
  public static CreateContactRequest CreateTestCreateContactRequest(
    string? contactName = default,
    string? contactEmail = default)
  =>
    new(
      contactName ?? GetRandomString(ContactConstraints.ContactNameMaxLength),
      contactEmail ?? GetRandomEmail(ContactConstraints.ContactEmailMaxLength)
    );
}