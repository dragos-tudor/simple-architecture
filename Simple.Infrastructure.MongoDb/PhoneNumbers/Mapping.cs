
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static void MapPhoneNumberClassType(BsonClassMap<PhoneNumber> classMap) =>
    MapClassType(classMap, (classMap) =>
      classMap
        .MapMember(pn => pn.NumberType)
        .SetSerializer(new EnumSerializer<PhoneNumberType>(BsonType.String))
    );
}