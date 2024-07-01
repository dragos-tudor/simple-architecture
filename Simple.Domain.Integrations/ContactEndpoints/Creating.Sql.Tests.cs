
using Microsoft.EntityFrameworkCore;

namespace Simple.Domain.Integrations;

partial class IntegrationsTests
{
  [TestMethod]
  public async Task new_contact_with_phone_number__create_sql_contact__contact_with_phone_number_created ()
  {
    using var dbContext = await SqlContextFactory.CreateDbContextAsync();
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);
    var messageQueue = CreateMessageQueue<Message>();

    var response = await CreateContactSqlEndpoint(contact, SqlContextFactory, messageQueue, new DefaultHttpContext(), Logger);

    var actual = await FindContactByKey(dbContext.Contacts.Include(c => c.PhoneNumbers).AsQueryable(), contact.ContactId).FirstOrDefaultAsync();
    Assert.AreEqual(actual, contact);
    Assert.IsInstanceOfType(response.Result, typeof(Created));
  }

  [TestMethod]
  public async Task new_contact_with_phone_number__create_sql_contact__contact_created_event_message_queued ()
  {
    using var dbContext = await SqlContextFactory.CreateDbContextAsync();
    var phoneNumber = CreateTestPhoneNumber();
    var contact = CreateTestContact(phoneNumbers: [phoneNumber]);
    var messageQueue = CreateMessageQueue<Message>();

    await CreateContactSqlEndpoint(contact, SqlContextFactory, messageQueue, new DefaultHttpContext(), Logger);

    var actual = await DequeueMessage(messageQueue);
    Assert.AreEqual(actual.MessageType, typeof(ContactCreatedEvent).Name);
  }
}