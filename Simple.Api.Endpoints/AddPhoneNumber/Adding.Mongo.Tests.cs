
using MongoDB.Driver;

namespace Simple.Api.Endpoints;

partial class EndpointsTests
{
  [TestMethod]
  public async Task contact_and_phone_number__add_mongo_phone_number_to_contact__phone_number_added_to_contact()
  {
    var contact = CreateTestContact();
    var contactColl = GetContactCollection(MongoDatabase);
    var request = CreateTestAddPhoneNumberRequest();
    await InsertContactAsync(contactColl, contact);

    var response = await AddPhoneNumberMongoAsync(contact.ContactId, request, MongoDatabase, CancellationToken.None);

    var actual = await FindContactById(contactColl.AsQueryable(), contact.ContactId).FirstOrDefaultAsync();
    AreEqual(actual!.PhoneNumbers, [CreatePhoneNumber(request.CountryCode, request.Number, request.Extension, request.NumberType)]);
    Assert.IsInstanceOfType(response.Result, typeof(Created));
  }
}