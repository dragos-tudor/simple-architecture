
#pragma warning disable CS4014
#pragma warning disable CA2201

using NSubstitute;

namespace Simple.Infrastructure.Queue;

partial class QueueTests
{
  [TestMethod]
  [DataRow(10)]
  [DataRow(100)]
  [DataRow(1000)]
  [DataRow(10000)]
  public void enqueue_messages_from_different_threads__dequeue_messages__messages_dequeueded(int queueCapacity)
  {
    var queue = CreateMessageQueue<int>(queueCapacity);
    using var counter = new CountdownEvent(queueCapacity);

    DequeueMessages(queue, (index, _) => { counter.Signal(); return Task.CompletedTask; }, (_, _) => {}, CancellationToken.None);
    Parallel.For(0, queueCapacity, (index, _) => { EnqueueMessage(index, queue); });

    counter.Wait();
  }

  [TestMethod]
  public async Task enqueue_error_throwing_message__dequeue_messages__errors_handled()
  {
    var queue = CreateMessageQueue<string>(1);
    var logger = Substitute.For<Action<string?, Exception>>();
    using var cts = new CancellationTokenSource();

    DequeueMessages(queue, (_, _) => { throw new Exception("error"); }, logger, cts.Token);
    EnqueueMessage("abc", queue);

    await cts.CancelAsync();
    logger.Received().Invoke(Arg.Is("abc"), Arg.Is<Exception>(ex => ex.Message == "error"));
  }
}