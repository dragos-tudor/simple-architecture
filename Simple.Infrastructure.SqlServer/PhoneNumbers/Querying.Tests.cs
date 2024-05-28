
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
 [TestMethod]
  public async Task phone_numbers__find_phone_number_by_country_code_and_number__stored_phone_number_with_filter ()
  {
    using var dbContext = CreateAgendaContext();
    PhoneNumber[] phoneNumbers = [
      CreateTestPhoneNumber(1, 123),
      CreateTestPhoneNumber(2, 123),
      CreateTestPhoneNumber(1, 789)
    ];
    var contact = CreateTestContact(phoneNumbers: phoneNumbers);

    AddContact(dbContext, contact);
    await SaveChangesAndClearContext(dbContext);

    var actual = await FindPhoneNumberByKey(dbContext.PhoneNumbers, 1, 123).SingleAsync();
    Assert.AreEqual(actual, phoneNumbers[0]);
  }

 [TestMethod]
  public async Task phone_numbers__find_phone_numbers_with_phone_numbers__stored_phone_numbers_with_filter ()
  {
    using var dbContext = CreateAgendaContext();
    PhoneNumber[] phoneNumbers = [
      CreateTestPhoneNumber(3, 123),
      CreateTestPhoneNumber(4, 123),
      CreateTestPhoneNumber(3, 789)
    ];
    var contact = CreateTestContact(phoneNumbers: phoneNumbers);

    AddContact(dbContext, contact);
    await SaveChangesAndClearContext(dbContext);

    var actual = await FindPhoneNumbersWithPhoneNumbers(dbContext.PhoneNumbers, [phoneNumbers[0], phoneNumbers[2]]);
    AreEqual(actual.ToArray(), [phoneNumbers[0], phoneNumbers[2]]);
  }
}