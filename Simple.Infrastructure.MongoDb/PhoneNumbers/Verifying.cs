
namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  static bool ExistPhoneNumber (PhoneNumber? phoneNumber) => phoneNumber is not null;
}