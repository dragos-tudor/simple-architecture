
using MongoDB.Bson.Serialization;

namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static void MapContactClassType (BsonClassMap<Contact> classMap) =>
    MapClassType(classMap, (classMap) => {
      classMap.MapIdMember(contact => contact.ContactId);
    });
}