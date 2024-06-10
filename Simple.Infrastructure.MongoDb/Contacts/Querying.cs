
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static IMongoQueryable<Contact> FindContactByEmail (IMongoQueryable<Contact> query, string contactEmail) =>
    query.Where(contact => contact.ContactEmail == contactEmail);

  public static IMongoQueryable<Contact> FindContactByKey (IMongoQueryable<Contact> query, Guid contactId) =>
    query.Where(contact => contact.ContactId == contactId);

  public static IMongoQueryable<Contact> FindContactByName (IMongoQueryable<Contact> query, string contactName) =>
    query.Where(contact => contact.ContactName == contactName);

  public static Task<Contact> FindContactByPhoneNumber (IMongoQueryable<Contact> query, PhoneNumber phoneNumber, CancellationToken cancellationToken = default) =>
    query.FirstOrDefaultAsync(contact => contact.PhoneNumbers.Any(_phoneNumber => _phoneNumber == phoneNumber), cancellationToken);

  public static IMongoQueryable<Contact> FindContacts (IMongoQueryable<Contact> query, int? pageSize, int? pageIndex) =>
    query.Page(pageSize ?? 0, pageIndex);
}