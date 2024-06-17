#pragma warning disable CS4014

using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Simple.Infrastructure.Queue;

partial class QueueTests
{
  static readonly ILogger Logger = Substitute.For<ILogger>();

  [TestMethod]
  [DataRow(10)]
  [DataRow(100)]
  [DataRow(1000)]
  [DataRow(10000)]
  public void enqueue_messages_from_different_threads__dequeue_messages__messages_dequeueded(int queueCapacity)
  {
    var queue = CreateMessageQueue<int>(queueCapacity);
    using var counter = new CountdownEvent(queueCapacity);

    DequeueMessages(queue, (message) => Task.FromResult(counter.Signal()), Substitute.For<HandleMessageError<int>>(), Logger, CancellationToken.None);
    Parallel.For(0, queueCapacity, (message, _) => { EnqueueMessage(queue, message); });

    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(3));
    counter.Wait(cancellationTokenSource.Token);
  }

  [TestMethod]
  public void enqueue_message_with_error_throwing_message_handlers__dequeue_messages__errors_handled()
  {
    var queueCapacity = 5;
    var queue = CreateMessageQueue<int>(queueCapacity);
    using var counter = new CountdownEvent(queueCapacity);

    DequeueMessages(queue, (message) => Task.FromException(new ArgumentException("")), (message, _) => Task.FromResult(counter.Signal()) , Logger, CancellationToken.None);
    Parallel.For(0, queueCapacity, (message, _) => { EnqueueMessage(queue, message); });

    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(3));
    counter.Wait(cancellationTokenSource.Token);
  }

  [TestMethod]
  public void enqueue_message_with_error_throwing_message_error_handlers__dequeue_messages__continue_dequeueing_messages()
  {
    var queueCapacity = 5;
    var queue = CreateMessageQueue<int>(queueCapacity);
    using var counter = new CountdownEvent(queueCapacity);

    DequeueMessages(queue, (message) => Task.FromException(new ArgumentException("")), (message, exception) => { counter.Signal(); return Task.FromException(exception); }, Logger, CancellationToken.None);
    Parallel.For(0, queueCapacity, (message, _) => { EnqueueMessage(queue, message); });

    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(3));
    counter.Wait(cancellationTokenSource.Token);
  }

  // TODO: generated consumern logging tests [without MockLogger]
  // https://github.com/nsubstitute/NSubstitute/issues/597
}