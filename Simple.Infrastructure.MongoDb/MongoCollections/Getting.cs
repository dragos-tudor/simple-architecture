
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  internal const string ContactCollectionName = "contacts";
  internal const string MessageCollectionName = "messages";
  static readonly WriteConcern DefaultWriteConcern = WriteConcern.WMajority;

  public static IMongoCollection<Contact> GetContactCollection (IMongoDatabase db, string collName = ContactCollectionName) =>
    GetMongoCollection<Contact> (db, collName).WithWriteConcern(DefaultWriteConcern);

  public static IMongoCollection<Message> GetMessageCollection (IMongoDatabase db, string collName = MessageCollectionName) =>
    GetMongoCollection<Message> (db, collName).WithWriteConcern(DefaultWriteConcern);
}