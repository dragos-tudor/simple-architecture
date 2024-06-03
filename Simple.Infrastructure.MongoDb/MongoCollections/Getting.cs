
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  static readonly WriteConcern DefaultWriteConcern = WriteConcern.WMajority;

  public static IMongoCollection<Contact> GetContactCollection (IMongoDatabase db) =>
    GetMongoCollection<Contact> (db, "contacts").WithWriteConcern(DefaultWriteConcern);

  public static IMongoCollection<Message> GetMessageCollection (IMongoDatabase db) =>
    GetMongoCollection<Message> (db, "messages").WithWriteConcern(DefaultWriteConcern);
}