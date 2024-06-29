
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static Task InsertPhoneNumberAsync (IMongoCollection<Contact> coll, Contact contact, PhoneNumber phoneNumber, CancellationToken cancellationToken = default)
  {
    var insertDefinition = AddToSetEachDefinition<Contact, PhoneNumber>(nameof(Contact.PhoneNumbers), [phoneNumber]);
    return UpdateDocument(coll, contact, insertDefinition, default, cancellationToken);
  }
}