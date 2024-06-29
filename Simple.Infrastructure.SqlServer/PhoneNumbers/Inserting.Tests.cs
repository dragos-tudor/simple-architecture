
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
 [TestMethod]
  public async Task contact_without_phone_numbers__insert_contact_phone_number__phone_number_added_on_contact ()
  {
    using var dbContext = CreateAgendaContext(SqlConnectionString);
    var contact = CreateTestContact();

    await InsertContactAsync(dbContext, contact);
    ClearChangeTracker(dbContext);

    await InsertPhoneNumberAsync(dbContext, contact, CreateTestPhoneNumber());
    ClearChangeTracker(dbContext);

    var actual = await FindContactByKey (dbContext.Contacts.Include(e => e.PhoneNumbers).AsQueryable(), contact.ContactId).SingleAsync();
    Assert.AreEqual(actual.PhoneNumbers[0], contact.PhoneNumbers[0]);
  }

 [TestMethod]
  public async Task contact_with_phone_numbers__insert_contact_phone_number__phone_number_added_on_contact ()
  {
    using var dbContext = CreateAgendaContext(SqlConnectionString);
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);

    await InsertContactAsync(dbContext, contact);
    ClearChangeTracker(dbContext);

    await InsertPhoneNumberAsync(dbContext, contact, CreateTestPhoneNumber());
    ClearChangeTracker(dbContext);

    var actual = await FindContactByKey (dbContext.Contacts.Include(e => e.PhoneNumbers).AsQueryable(), contact.ContactId).SingleAsync();
    CollectionAssert.AreEqual(actual.PhoneNumbers.OrderBy(e => e.Number).ToArray(), contact.PhoneNumbers.OrderBy(e => e.Number).ToArray());
  }
}