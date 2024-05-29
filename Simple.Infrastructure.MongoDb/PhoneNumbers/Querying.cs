
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static async Task<PhoneNumber?> FindPhoneNumber (IMongoCollection<Contact> coll, PhoneNumber phoneNumber, CancellationToken cancellationToken = default) =>
    (await coll
      .Find(Builders<Contact>.Filter.AnyEq(contact => contact.PhoneNumbers, phoneNumber))
      .FirstOrDefaultAsync(cancellationToken)
    )?.PhoneNumbers
        .Where(pn => phoneNumber == pn)
        .FirstOrDefault();

}