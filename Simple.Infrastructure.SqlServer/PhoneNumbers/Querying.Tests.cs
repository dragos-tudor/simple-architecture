
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
  [TestMethod]
  public async Task phone_numbers__find_phone_number__filtered_phone_number()
  {
    using var dbContext = CreateAgendaContext(SqlConnectionString);
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);

    AddContact(dbContext, contact);
    AddPhoneNumber(dbContext, phoneNumber);
    await SaveChangesAsync(dbContext);
    ClearChangeTracker(dbContext);

    var actual = await FindPhoneNumber(dbContext.PhoneNumbers.AsQueryable(), phoneNumber).SingleAsync();
    Assert.AreEqual(actual, phoneNumber);
  }
}