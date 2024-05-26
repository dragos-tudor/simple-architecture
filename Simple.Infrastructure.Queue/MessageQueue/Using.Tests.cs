
#pragma warning disable CS4014

namespace Simple.Infrastructure.Queue;

partial class QueueTests
{
  [TestMethod]
  [DataRow(10)]
  [DataRow(100)]
  [DataRow(1000)]
  [DataRow(10000)]
  public async Task enqueue_messages_from_different_threads__consume_messages__messages_consumed(int noOfMessages)
  {
    var queue = CreateMessageQueue<int>(noOfMessages);
    using var cts = new CancellationTokenSource();
    MessageHandler<int> handleMessage = (msg, _) => Task.FromResult(true);
    Func<int, CancellationToken, ValueTask> enqueueMessage = async (index, _) => {
      EnqueueMessage(index, queue);
      if(index == noOfMessages) await cts.CancelAsync();
    };

    ConsumeMessages(queue, handleMessage, cts.Token);
    await Parallel.ForAsync(0, noOfMessages, enqueueMessage);
  }

  [TestMethod]
  public async Task error_throwing_message_handler__enqueue_messages__errors_handled()
  {
    var queue = CreateMessageQueue<string>(1);
    using var cts = new CancellationTokenSource();
    MessageHandler<string> handleMessage = (msg, _) => { throw new ArgumentException(msg); };
    var logger = new FakeLogger((level, message) => { if(IsErrorLogLevel(level)) throw new ArgumentException(message); });

    EnqueueMessage("abc", queue);
    await Assert.ThrowsExceptionAsync<ArgumentException>(() => ConsumeMessages(queue, handleMessage, cts.Token, logger), "Consumer: consuming messages error abc.");
  }
}