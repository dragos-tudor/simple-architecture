
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  static IMongoClient GetMongoClient<T> (IMongoCollection<T> coll) => coll.Database.Client;
}