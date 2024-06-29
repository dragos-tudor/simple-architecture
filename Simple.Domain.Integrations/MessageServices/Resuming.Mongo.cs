
using MongoDB.Driver;

namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  internal static async Task<IEnumerable<Message>> ResumeMessagesPageMongoAsync (IMongoDatabase mongoDatabase, Channel<Message> messageQueue, DateTime minDate, DateTime maxDate, int batchSize, CancellationToken cancellationToken = default)
  {
    var messageCollection = GetMessageCollection(mongoDatabase);
    var messages = await FindResumableMessages(messageCollection.AsQueryable(), minDate, maxDate, batchSize).ToListAsync(cancellationToken);

    foreach(var message in messages)
      EnqueueMessage(messageQueue, message);
    return messages;
  }

  public static async Task ResumeMessagesMongoAsync (IMongoDatabase mongoDatabase, Channel<Message> messageQueue, TimeProvider timeProvider, ResumeMessagesOptions resumeMessagesOptions, CancellationToken cancellationToken = default)
  {
    IEnumerable<Message> messages = [];
    var currentDate = timeProvider.GetUtcNow().UtcDateTime;
    var minDate = GetMinResumeDate(currentDate, resumeMessagesOptions);
    var maxDate = GetMaxResumeDate(currentDate, resumeMessagesOptions);
    var batchSize = resumeMessagesOptions.BatchSize;

    do { messages = await ResumeMessagesPageMongoAsync(mongoDatabase, messageQueue, GetLastMessageDate(messages) ?? minDate, maxDate, batchSize, cancellationToken); }
    while (messages.Any());
  }
}