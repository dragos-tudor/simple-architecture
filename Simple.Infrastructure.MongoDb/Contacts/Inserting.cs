
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static Task InsertContact (IClientSessionHandle session, IMongoCollection<Contact> coll, Contact contact, CancellationToken cancellationToken = default) =>
    InsertDocument(session, coll, contact, default, cancellationToken);

  public static Task InsertContact (IMongoCollection<Contact> coll, Contact contact, CancellationToken cancellationToken = default) =>
    InsertDocument(coll, contact, default, cancellationToken);

  public static Task InsertContactPhoneNumber (IMongoCollection<Contact> coll, Contact contact, PhoneNumber phoneNumber, CancellationToken cancellationToken = default) =>
    UpdateDocument(coll, contact,
      AddToSetEachDefinition<Contact, PhoneNumber>(nameof(Contact.PhoneNumbers), [phoneNumber]), default, cancellationToken);
}