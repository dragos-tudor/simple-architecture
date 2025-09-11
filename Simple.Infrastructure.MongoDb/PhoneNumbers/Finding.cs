
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static async Task<PhoneNumber?> FindMongoPhoneNumber(IMongoCollection<Contact> contacts, PhoneNumber phoneNumber, CancellationToken cancellationToken)
  {
    var contact = await FindContactPhoneNumber(contacts.AsQueryable(), phoneNumber).FirstOrDefaultAsync(cancellationToken);
    return contact is not null ?
      FindPhoneNumber(contact.PhoneNumbers.AsQueryable(), phoneNumber).First() :
      default;
  }
}