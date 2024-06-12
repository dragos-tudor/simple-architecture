
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static Task InsertContactPhoneNumber (IMongoCollection<Contact> coll, Contact contact, PhoneNumber phoneNumber, CancellationToken cancellationToken = default) =>
    UpdateDocument(coll, contact,
      AddToSetEachDefinition<Contact, PhoneNumber>(nameof(Contact.PhoneNumbers), [phoneNumber]), default, cancellationToken);
}