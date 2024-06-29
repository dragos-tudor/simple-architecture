
using Microsoft.Extensions.Time.Testing;

namespace Simple.Domain.Integrations;

partial class IntegrationsTests
{
  [TestMethod]
  public async Task old_messages__resume_mongo_messages__old_messages_resumed ()
  {
    var currentDate = DateTime.UtcNow.AddYears(-5);
    var messageQueue = CreateMessageQueue<Message>(10);
    var resumeMessagesOption = new ResumeMessagesOptions(){ BatchSize = 2, MinTime = TimeSpan.FromSeconds(5), MaxTime = TimeSpan.Zero };
    Message[] messages = [
      CreateMessage(new AddedToAgendaNotification(), messageDate: currentDate.AddSeconds(-3), isActive: false),
      CreateMessage(new AddedToAgendaNotification(), messageDate: currentDate.AddSeconds(-2)),
      CreateMessage(new AddedToAgendaNotification(), messageDate: currentDate.AddSeconds(-1)),
      CreateMessage(new AddedToAgendaNotification(), messageDate: currentDate),
      CreateMessage(new AddedToAgendaNotification(), messageDate: currentDate.AddSeconds(1))
    ];

    await InsertMessageAsync(GetMessageCollection(MongoDatabase), messages[0]);
    await InsertMessageAsync(GetMessageCollection(MongoDatabase), messages[1]);
    await InsertMessageAsync(GetMessageCollection(MongoDatabase), messages[2]);
    await InsertMessageAsync(GetMessageCollection(MongoDatabase), messages[3]);
    await InsertMessageAsync(GetMessageCollection(MongoDatabase), messages[4]);

    await ResumeMessagesMongoAsync(MongoDatabase, messageQueue, new FakeTimeProvider(currentDate), resumeMessagesOption);
    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(10));
    Message[] actual = [
      await DequeueMessage(messageQueue, cancellationTokenSource.Token),
      await DequeueMessage(messageQueue, cancellationTokenSource.Token),
      await DequeueMessage(messageQueue, cancellationTokenSource.Token)
    ];
    Assert.AreEqual(actual[0].MessageId, messages[1].MessageId); // records not equal with same key!
    Assert.AreEqual(actual[1].MessageId, messages[2].MessageId);
    Assert.AreEqual(actual[2].MessageId, messages[3].MessageId);
  }
}