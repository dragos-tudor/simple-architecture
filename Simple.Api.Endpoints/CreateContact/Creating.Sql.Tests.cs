
using Microsoft.EntityFrameworkCore;

namespace Simple.Api.Endpoints;

partial class EndpointsTests
{
  [TestMethod]
  public async Task contact__create_sql_contact__contact_created()
  {
    using var dbContext = CreateAgendaContext(SqlConnectionString);
    var request = CreateTestCreateContactRequest();
    var messageQueue = CreateMessageQueue<Message>();

    var response = await CreateContactSqlAsync(request, SqlContextFactory, messageQueue, "", CancellationToken.None);

    var contact = await FindContactByName(dbContext.Contacts.AsQueryable(), request.ContactName).FirstOrDefaultAsync();
    Assert.IsNotNull(contact);
    Assert.IsInstanceOfType(response.Result, typeof(Created));
  }

  [TestMethod]
  public async Task contact__create_sql_contact__contact_created_event_message_queued()
  {
    using var dbContext = CreateAgendaContext(SqlConnectionString);
    var request = CreateTestCreateContactRequest();
    var messageQueue = CreateMessageQueue<Message>();

    await CreateContactSqlAsync(request, SqlContextFactory, messageQueue, "", CancellationToken.None);

    var message = await DequeueMessage(messageQueue);
    Assert.AreEqual(message.MessageType, nameof(ContactCreatedEvent));
  }
}