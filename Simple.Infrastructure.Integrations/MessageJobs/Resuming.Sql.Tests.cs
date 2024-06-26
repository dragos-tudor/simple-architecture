
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Time.Testing;

namespace Simple.Infrastructure.Integrations;

partial class IntegrationsTests
{
  [TestMethod]
  public async Task old_messages__resume_sql_messages__old_messages_resumed ()
  {
    using var dbContext = await SqlContextFactory.CreateDbContextAsync();
    var currentDate = DateTime.UtcNow.AddYears(-5);
    var messageQueue = CreateMessageQueue<Message>(10);
    var resumeMessagesOption = new ResumeMessagesOptions(){ BatchSize = 2, MinTime = TimeSpan.FromSeconds(5), MaxTime = TimeSpan.Zero };
    Message[] messages = [
      CreateMessage(CreateAddedToAgendaNotification("", ""), messageDate: currentDate.AddSeconds(-3), isActive: false),
      CreateMessage(CreateAddedToAgendaNotification("", ""), messageDate: currentDate.AddSeconds(-2)),
      CreateMessage(CreateAddedToAgendaNotification("", ""), messageDate: currentDate.AddSeconds(-1)),
      CreateMessage(CreateAddedToAgendaNotification("", ""), messageDate: currentDate),
      CreateMessage(CreateAddedToAgendaNotification("", ""), messageDate: currentDate.AddSeconds(1))
    ];

    await InsertMessageAsync(dbContext, messages[0]);
    await InsertMessageAsync(dbContext, messages[1]);
    await InsertMessageAsync(dbContext, messages[2]);
    await InsertMessageAsync(dbContext, messages[3]);
    await InsertMessageAsync(dbContext, messages[4]);

    await ResumeMessagesSqlAsync(SqlContextFactory, messageQueue, new FakeTimeProvider(currentDate), resumeMessagesOption, NullLoggerFactory.Instance.CreateLogger(""));
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