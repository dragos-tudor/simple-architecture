
using MongoDB.Bson.Serialization.IdGenerators;

namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  static readonly CombGuidGenerator SequentialGuidGenerator = new ();

  public static Guid GenerateSequentialGuid () => SequentialGuidGenerator.NewCombGuid(Guid.NewGuid(), DateTime.Now);
}