
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
  public static Contact CreateTestContact (
    Guid? contactId = default,
    string? contactName = default,
    string? contactEmail = default,
    params PhoneNumber[] phoneNumbers)
  =>
    new () {
      ContactId = contactId ?? GetRandomGuid(),
      ContactName = contactName ?? GetRandomString(ContactConstraints.ContactNameMaxLength),
      ContactEmail = contactEmail ?? GetRandomString(ContactConstraints.ContactEmailMaxLength),
      PhoneNumbers = phoneNumbers
    };

  public static Message CreateTestMessage (
    Guid? messageId = default,
    string? messageType = default,
    string? messageContent = default,
    DateTime? messageData = default,
    Guid? parentId = default,
    string? traceId = default,
    bool? isActive = default)
  =>
    new () {
      MessageId = messageId ?? GetRandomGuid(),
      MessageType = messageType ?? GetRandomString(MessageContraints.MessageTypeMaxLength),
      MessageContent = messageContent ?? GetRandomString(50),
      MessageDate = messageData ?? GetRandomDate(),
      ParentId = parentId ?? GetRandomGuid(),
      TraceId = traceId ?? GetRandomString(MessageContraints.TraceIdMaxLength),
      IsActive = isActive ?? GetRandomInt(0, 1) == 1? true: false
    };

  public static PhoneNumber CreateTestPhoneNumber (
    string? countryCode = default,
    string? number = default,
    string? extension = default,
    PhoneNumberType? phoneNumberType = PhoneNumberType.Mobile)
  =>
    new () {
      CountryCode = countryCode ?? GetRandomString(PhoneNumberContraints.CountryCodeMaxLength),
      Number = number ?? GetRandomString(PhoneNumberContraints.NumberLength),
      NumberType = phoneNumberType ?? Enum.GetValues<PhoneNumberType>()[GetRandomInt(0, 2)],
      Extension = extension ?? GetRandomString(PhoneNumberContraints.ExtensionMaxLength)
    };

}
