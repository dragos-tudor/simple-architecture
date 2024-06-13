
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static Task DeletePhoneNumber (IMongoCollection<Contact> coll, Contact contact, PhoneNumber phoneNumber, CancellationToken cancellationToken = default)
  {
    var storedPhoneNumber = ModelsFuncs.FindPhoneNumber((contact.PhoneNumbers ?? []).AsQueryable(), phoneNumber).FirstOrDefault();
    if(!ExistPhoneNumber(storedPhoneNumber)) return Task.CompletedTask;

    var deleteDefinition = PullOneFromSetDefinition<Contact, PhoneNumber>(nameof(Contact.PhoneNumbers), storedPhoneNumber!);
    return UpdateDocument(coll, contact, deleteDefinition, default, cancellationToken);
  }
}