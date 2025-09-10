
using MongoDB.Driver;

namespace Simple.Api.Endpoints;

partial class EndpointsTests
{
  [TestMethod]
  public async Task contact_with_phone_number__delete_mongo_phone_number__phone_number_deleted()
  {
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);
    var contactColl = GetContactCollection(MongoDatabase);
    await InsertContactAsync(contactColl, contact);

    await DeletePhoneNumberMongoAsync(contact.ContactId, phoneNumber.CountryCode, phoneNumber.Number, MongoDatabase, CancellationToken.None);

    var actual = await FindContactById(contactColl.AsQueryable(), contact.ContactId).FirstOrDefaultAsync();
    AreEqual(actual!.PhoneNumbers, []);
  }
}