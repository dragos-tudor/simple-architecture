
#pragma warning disable CS4014

using NSubstitute;

namespace Simple.Infrastructure.Queue;

partial class QueueTests
{
  [TestMethod]
  [DataRow(10)]
  [DataRow(100)]
  [DataRow(1000)]
  [DataRow(10000)]
  public void enqueue_messages_from_different_threads__consume_messages__messages_consumed(int queueCapacity)
  {
    var queue = CreateMessageQueue<int>(queueCapacity);
    using var counter = new CountdownEvent(queueCapacity);

    DequeueMessages(queue, (index, _) => { counter.Signal(); return Task.FromResult(true); }, (_) => default!, CancellationToken.None);
    Parallel.For(0, queueCapacity, (index, _) => { EnqueueMessage(index, queue); });

    counter.Wait();
  }

  [TestMethod]
  public async Task error_throwing_message_handler__enqueue_messages__errors_handled()
  {
    var queue = CreateMessageQueue<string>(1);
    var logger = Substitute.For<Func<Exception, string>>();
    using var cts = new CancellationTokenSource();

    DequeueMessages(queue, (message, _) => { throw new ArgumentException(message); }, logger, cts.Token);
    EnqueueMessage("abc", queue);

    await cts.CancelAsync();
    logger.Received().Invoke(Arg.Is<ArgumentException>(ex => ex.Message == "abc"));
  }
}