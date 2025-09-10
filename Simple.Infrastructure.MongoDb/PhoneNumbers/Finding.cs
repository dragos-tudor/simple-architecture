
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static async Task<PhoneNumber?> FindMongoPhoneNumber(IMongoCollection<Contact> contacts, PhoneNumber phoneNumber, CancellationToken cancellationToken)
  {
    var contact = await FindContactPhoneNumber(contacts.AsQueryable(), phoneNumber).FirstOrDefaultAsync(cancellationToken);
    if (contact is null) return default;

    var phoneNumbers = contact.PhoneNumbers;
    return FindPhoneNumber(phoneNumbers.AsQueryable(), phoneNumber).First();
  }
}