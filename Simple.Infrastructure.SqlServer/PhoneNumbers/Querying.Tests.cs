
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
 [TestMethod]
  public async Task phone_numbers__get_phone_number_by_code_and_number__stored_phone_number ()
  {
    using var dbContext = CreateAgendaContext();
    PhoneNumber[] phoneNumbers = [
      CreateTestPhoneNumber("+1", 123),
      CreateTestPhoneNumber("+2", 123),
      CreateTestPhoneNumber("+1", 789)
    ];
    var contact = CreateTestContact(phoneNumbers: phoneNumbers);

    AddContact(dbContext, contact);
    await SaveChangesAndClearContext(dbContext);

    var actual = await FindPhoneNumberByCountryCodeAndNumber(dbContext.PhoneNumbers, "+1", 123).SingleAsync();
    Assert.AreEqual(actual, phoneNumbers[0]);
  }
}