
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static IMongoQueryable<Contact> FindContactByKey (IMongoQueryable<Contact> query, Guid contactId) =>
    query.Where(contact => contact.ContactId == contactId);

  public static IMongoQueryable<Contact> FindContactByName (IMongoQueryable<Contact> query, string contactName) =>
    query.Where(contact => contact.ContactName == contactName);
}