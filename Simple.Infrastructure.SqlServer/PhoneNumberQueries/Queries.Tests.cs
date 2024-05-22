using SqlFuncs = Storing.SqlServer.SqlServerFuncs;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
 [TestMethod]
  public async Task phone_numbers__find_by_phone_number__filtered_phone_numbers ()
  {
    using var dbContext = CreateAgendaContext();
    PhoneNumber[] phoneNumbers = [
      new PhoneNumber() { CountryCode = "+1", Number = "123", NumberType = PhoneNumberType.Mobile },
      new PhoneNumber() { CountryCode = "+2", Number = "123", NumberType = PhoneNumberType.Mobile },
      new PhoneNumber() { CountryCode = "+1", Number = "789", NumberType = PhoneNumberType.Home }
    ];
    var contact = new Contact() {
      ContactEmail = Guid.NewGuid().ToString(),
      ContactName = Guid.NewGuid().ToString(),
      PhoneNumbers = phoneNumbers
    };
    SqlFuncs.AddEntity(dbContext, contact);
    foreach(var phoneNumber in phoneNumbers)
      SqlFuncs.AddEntity(dbContext, phoneNumber);
    await dbContext.SaveChangesAsync();

    var actual = await GetContactPhoneNumber(dbContext.PhoneNumbers, "123", "+1").FirstAsync();
    AreEqual(actual.Number, "123");
    AreEqual(actual.CountryCode, "+1");
  }

}