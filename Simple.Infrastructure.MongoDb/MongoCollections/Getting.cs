
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  static readonly WriteConcern DefaultWriteConcern = WriteConcern.WMajority;

  public static IMongoCollection<T> GetMongoCollection<T> (IMongoDatabase db, string collName)  =>
    GetCollection<T>(db, collName);

  public static IMongoCollection<Contact> GetContactCollection (IMongoDatabase db, string coll = "contacts") =>
    GetMongoCollection<Contact> (db, coll).WithWriteConcern(DefaultWriteConcern);

  public static IMongoCollection<Message> GetMessageCollection (IMongoDatabase db, string coll = "messages") =>
    GetMongoCollection<Message> (db, coll).WithWriteConcern(DefaultWriteConcern);
}