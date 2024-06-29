
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static void MapMessageClassType (BsonClassMap<Message> classMap) =>
    MapClassType(classMap, (classMap) => {
      classMap.MapIdMember(message => message.MessageId).SetIdGenerator(new CombGuidGenerator());
      classMap.UnmapField(message => message.MessageContent);
    });

  public static void MapMessageClassType<T> (BsonClassMap<Message<T>> classMap) =>
    MapClassType(classMap, (classMap) => {
      classMap.SetDiscriminator(typeof(T).Name);
    });
}