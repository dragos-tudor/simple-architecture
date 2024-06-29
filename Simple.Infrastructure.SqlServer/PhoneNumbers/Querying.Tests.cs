
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
 [TestMethod]
  public async Task phone_numbers__find_phone_number__filtered_phone_number ()
  {
    using var dbContext = CreateAgendaContext(SqlConnectionString);
    PhoneNumber[] phoneNumbers = [
      CreateTestPhoneNumber(),
      CreateTestPhoneNumber(),
      CreateTestPhoneNumber()
    ];
    var contact = CreateTestContact(phoneNumbers: phoneNumbers);

    await InsertContactAsync(dbContext, contact);
    ClearChangeTracker(dbContext);

    var actual = await FindPhoneNumber(dbContext.PhoneNumbers.AsQueryable(), phoneNumbers[0]).SingleAsync();
    Assert.AreEqual(actual, phoneNumbers[0]);
  }

 [TestMethod]
  public async Task phone_numbers__find_phone_numbers_with_phone_numbers__filtered_phone_numbers ()
  {
    using var dbContext = CreateAgendaContext(SqlConnectionString);
    var phoneNumbers = new PhoneNumber[] {
      CreateTestPhoneNumber(),
      CreateTestPhoneNumber(),
      CreateTestPhoneNumber()
     }.OrderBy(e => e.CountryCode).ToArray();
    var contact = CreateTestContact(phoneNumbers: phoneNumbers);

    await InsertContactAsync(dbContext, contact);
    ClearChangeTracker(dbContext);

    var actual = await FindPhoneNumbers(dbContext.PhoneNumbers.AsQueryable(), [phoneNumbers[0], phoneNumbers[2]]);
    CollectionAssert.AreEqual(actual.ToArray(), (PhoneNumber[])[phoneNumbers[0], phoneNumbers[2]]);
  }
}