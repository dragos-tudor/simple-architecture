
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
 [TestMethod]
  public async Task contact_without_phone_numbers__insert_contact_phone_number__phone_number_added_on_contact ()
  {
    using var dbContext = CreateAgendaContext(AgendaConnString);
    var contact = CreateTestContact();

    await InsertContact(dbContext, contact);
    ClearChangeTracker(dbContext);

    await InsertContactPhoneNumber(dbContext, contact, CreateTestPhoneNumber());
    ClearChangeTracker(dbContext);

    var actual = await FindContactByKey (dbContext.Contacts.Include(e => e.PhoneNumbers).AsQueryable(), contact.ContactId).SingleAsync();
    Assert.AreEqual(actual.PhoneNumbers[0], contact.PhoneNumbers[0]);
  }

 [TestMethod]
  public async Task contact_with_phone_numbers__insert_contact_phone_number__phone_number_added_on_contact ()
  {
    using var dbContext = CreateAgendaContext(AgendaConnString);
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);

    await InsertContact(dbContext, contact);
    ClearChangeTracker(dbContext);

    await InsertContactPhoneNumber(dbContext, contact, CreateTestPhoneNumber());
    ClearChangeTracker(dbContext);

    var actual = await FindContactByKey (dbContext.Contacts.Include(e => e.PhoneNumbers).AsQueryable(), contact.ContactId).SingleAsync();
    CollectionAssert.AreEqual(actual.PhoneNumbers.OrderBy(e => e.Number).ToArray(), contact.PhoneNumbers.OrderBy(e => e.Number).ToArray());
  }
}