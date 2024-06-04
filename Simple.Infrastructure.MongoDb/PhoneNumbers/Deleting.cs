
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static Task DeletePhoneNumber (IMongoCollection<Contact> coll, Contact contact, PhoneNumber phoneNumber, CancellationToken cancellationToken = default) =>
    UpdateDocument(coll, contact,
      PullOneFromSetDefinition<Contact, PhoneNumber>(nameof(Contact.PhoneNumbers), phoneNumber), default, cancellationToken);
}