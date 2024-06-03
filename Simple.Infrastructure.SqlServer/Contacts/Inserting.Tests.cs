
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
 [TestMethod]
  public async Task new_contact_and_message__insert_contact_and_message__contact_and_message_stored ()
  {
    using var dbContext = CreateAgendaContext(AgendaConnString);
    var contact = CreateTestContact();
    var message = CreateTestMessage();

    await InsertContactAndMessage(dbContext, contact, message);
    ClearChangeTracker(dbContext);

    Assert.AreEqual(await FindContactByKey (dbContext.Contacts, contact.ContactId).SingleAsync(), contact);
    Assert.AreEqual(await FindMessageByKey (dbContext.Messages, message.MessageId).SingleAsync(), message);
  }
}