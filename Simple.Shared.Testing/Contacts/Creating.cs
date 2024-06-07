
namespace Simple.Shared.Testing;

partial class TestingFuncs
{
  public static Contact CreateTestContact (
    Guid? contactId = default,
    string? contactName = default,
    string? contactEmail = default,
    PhoneNumber[]? phoneNumbers = default)
  =>
    new () {
      ContactId = contactId ?? GetRandomGuid(),
      ContactName = contactName ?? GetRandomString(ContactConstraints.ContactNameMaxLength),
      ContactEmail = contactEmail ?? GetRandomEmail(ContactConstraints.ContactEmailMaxLength),
      PhoneNumbers = new List<PhoneNumber>(phoneNumbers ?? [])
    };
}
