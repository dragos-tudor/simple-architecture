using MongoDB.Bson.Serialization;

namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static void MapClassType<T> (BsonClassMap<T> classMap, Action<BsonClassMap<T>>? additionallyMapping = default) {
    classMap.AutoMap();
    additionallyMapping?.Invoke(classMap);
    classMap.SetIgnoreExtraElements(true);
    classMap.Freeze();
  }

  public static void MapModelClassTypes () {
    BsonClassMap.RegisterClassMap<Contact>(MapContactClassType);
    BsonClassMap.RegisterClassMap<PhoneNumber>(MapPhoneNumberClassType);
    BsonClassMap.RegisterClassMap<Message>(MapMessageClassType);
  }
}