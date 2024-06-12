
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
 [TestMethod]
  public async Task new_contact_with_phone_numbers__insert_contact__contact_and_phone_numbers_stored ()
  {
    using var dbContext = CreateAgendaContext(AgendaConnString);
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);
    var message = CreateTestMessage();

    await InsertContactAndMessage(dbContext, contact, message);
    ClearChangeTracker(dbContext);

    var actual = await FindContactByKey (dbContext.Contacts.AsQueryable(), contact.ContactId).Include(c => c.PhoneNumbers).SingleAsync();
    Assert.AreEqual(actual, contact);
    Assert.AreEqual(actual.PhoneNumbers[0], phoneNumber);
  }

 [TestMethod]
  public async Task new_contact_and_message__insert_contact_and_message__contact_and_message_stored ()
  {
    using var dbContext = CreateAgendaContext(AgendaConnString);
    var contact = CreateTestContact();
    var message = CreateTestMessage();

    await InsertContactAndMessage(dbContext, contact, message);
    ClearChangeTracker(dbContext);

    Assert.AreEqual(await FindContactByKey (dbContext.Contacts.AsQueryable(), contact.ContactId).SingleAsync(), contact);
    Assert.AreEqual(await FindMessageByKey (dbContext.Messages.AsQueryable(), message.MessageId).SingleAsync(), message);
  }
}