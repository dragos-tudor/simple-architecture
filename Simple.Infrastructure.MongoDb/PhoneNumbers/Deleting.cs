
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  static UpdateDefinition<Contact> BuildDeletePhoneNumberDefinition(PhoneNumber phoneNumber) =>
    Builders<Contact>.Update.PullFilter(
        contact => contact.PhoneNumbers,
        _phoneNumber => _phoneNumber.CountryCode == phoneNumber.CountryCode && _phoneNumber.Number == phoneNumber.Number
    );

  public static Task DeletePhoneNumberAsync(IMongoCollection<Contact> coll, Contact contact, PhoneNumber phoneNumber, CancellationToken cancellationToken = default)
  {
    // var deleteDefinition = PullOneFromSetDefinition<Contact, PhoneNumber>(nameof(Contact.PhoneNumbers), phoneNumber!);
    var deleteDefinition = BuildDeletePhoneNumberDefinition(phoneNumber);

    return UpdateDocument(coll, contact, deleteDefinition, default, cancellationToken);
  }
}