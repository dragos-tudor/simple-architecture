
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static Task DeletePhoneNumberAsync (IMongoCollection<Contact> coll, Contact contact, PhoneNumber phoneNumber, CancellationToken cancellationToken = default)
  {
    var deleteDefinition = PullOneFromSetDefinition<Contact, PhoneNumber>(nameof(Contact.PhoneNumbers), phoneNumber!);
    return UpdateDocument(coll, contact, deleteDefinition, default, cancellationToken);
  }
}