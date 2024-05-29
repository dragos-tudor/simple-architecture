
using MongoDB.Bson.Serialization;

namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static void MapMessageClassType (BsonClassMap<Message> classMap) =>
    MapClassType(classMap, (classMap) => {
      classMap.MapIdMember(message => message.MessageId);
      classMap.UnmapMember(message => message.MessageContent);
    });
}