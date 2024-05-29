
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static MongoUrlBuilder SetMongoUrlBuilder (MongoUrlBuilder urlBuilder)
  {
    urlBuilder.ConnectTimeout = TimeSpan.FromSeconds(10);
    urlBuilder.ServerSelectionTimeout = TimeSpan.FromSeconds(15);
    return urlBuilder;
  }

  public static MongoClient CreateMongoClient (string connString) =>
    MongoFuncs.CreateMongoClient(connString, SetMongoUrlBuilder);
}