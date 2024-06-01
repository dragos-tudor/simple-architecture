
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static async Task<PhoneNumber?> FindPhoneNumber (IMongoQueryable<Contact> query, PhoneNumber phoneNumber, CancellationToken cancellationToken = default) =>
    await FindContactByPhoneNumber(query, phoneNumber, cancellationToken) is not null? phoneNumber: default;

  public static async Task<IEnumerable<PhoneNumber>> FindPhoneNumbers (IMongoQueryable<Contact> query, IEnumerable<PhoneNumber> phoneNumbers, CancellationToken cancellationToken = default) =>
    (await Task.WhenAll(phoneNumbers.Select(phoneNumber => FindPhoneNumber(query, phoneNumber, cancellationToken))))
      .Where(ExistPhoneNumber)!;
}