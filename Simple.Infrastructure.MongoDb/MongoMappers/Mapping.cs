using MongoDB.Bson.Serialization;

namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static void MapModelClassTypes () {
    BsonClassMap.RegisterClassMap<Contact>(MapContactClassType);
    BsonClassMap.RegisterClassMap<PhoneNumber>(MapPhoneNumberClassType);
    BsonClassMap.RegisterClassMap<Message>(MapMessageClassType);
    BsonClassMap.RegisterClassMap<Message<AddedToAgendaNotification>>(MapMessageClassType);
    BsonClassMap.RegisterClassMap<Message<RemovedFromAgendaNotification>>(MapMessageClassType);
  }
}