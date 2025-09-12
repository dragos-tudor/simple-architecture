
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace Simple.Messaging.Handlers;

partial class HandlersTests
{
  [TestMethod]
  public async Task contact_created_message__handle_sql_message__finalized_continuation_message()
  {
    var dbContext = CreateAgendaContext(SqlConnectionString);
    var message = CreateTestMessage(CreateContactCreatedEvent(Guid.NewGuid(), GetRandomEmail(10)));
    await HandleContactCreatedSqlAsync(message, dbContext, MailServerOptions, DateTime.UtcNow);

    var continuationMessage = await FindMessageByParentId(dbContext.Messages.AsQueryable(), message.MessageId).FirstAsync();
    Assert.AreEqual(continuationMessage!.MessageType, nameof(NotificationSentEvent));
    Assert.IsFalse(continuationMessage.IsPending);
  }

  [TestMethod]
  public async Task contact_created_message__handle_sql_message__notification_sent()
  {
    var dbContext = CreateAgendaContext(SqlConnectionString);
    var message = CreateTestMessage(CreateContactCreatedEvent(Guid.NewGuid(), GetRandomEmail(10)));
    await HandleContactCreatedSqlAsync(message, dbContext, MailServerOptions, DateTime.UtcNow);

    var actual = await ReceiveMailMessagesAsync(message.MessagePayload.ContactEmail, message.MessagePayload.ContactEmail, MailServerOptions);

    var filterMail = (MimeMessage _mailMessage) => GetMessageTo(_mailMessage) == message.MessagePayload.ContactEmail;
    Assert.IsNotNull(actual.Where(filterMail).Select(GetMessageFrom).FirstOrDefault());
  }
}