#pragma warning disable CS4014

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Simple.Infrastructure.MessageQueue;

partial class QueueTests
{
  static readonly ILogger Logger = new NullLoggerFactory().CreateLogger("");

  [TestMethod]
  [DataRow(10)]
  [DataRow(100)]
  [DataRow(1000)]
  [DataRow(10000)]
  public void enqueue_messages_from_different_threads__process_messages__messages_processed(int queueCapacity)
  {
    var queue = CreateMessageQueue<Message>(queueCapacity);
    using var counter = new CountdownEvent(queueCapacity);

    ProcessMessage<Message> processMessage = (message, _) => Task.FromResult(counter.Signal());
    HandleMessageError<Message> handleMessageError = (_, __, ___) => Task.CompletedTask;

    ProcessMessagesAsync(queue, processMessage, handleMessageError, Logger, CancellationToken.None);
    Parallel.For(0, queueCapacity, (message, _) => { EnqueueMessage(queue, CreateTestMessage()); });

    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(3));
    counter.Wait(cancellationTokenSource.Token);
  }

  [TestMethod]
  public async Task message_and_error_throwing_message_handler__process_message__error_handled()
  {
    ProcessMessage<Message> processMessage = (_, __) => Task.FromException(new ArgumentException("error"));
    HandleMessageError<Message> handleMessageError = (_, exception, ___) => Task.FromResult(exception);

    var actual = await ProcessMessageAsync(CreateTestMessage(), processMessage, handleMessageError, Logger, CancellationToken.None);

    Assert.AreEqual("error", actual!.Message);
  }

  [TestMethod]
  public async Task message_and_error_throwing_message_error_handler__process_message__inner_error_handled()
  {
    ProcessMessage<Message> processMessage = (_, _) => Task.FromException(new ArgumentException("error"));
    HandleMessageError<Message> handleMessageError = (_, exception, __) => Task.FromException(exception);

    var actual = await ProcessMessageAsync(CreateTestMessage(), processMessage, handleMessageError, Logger, CancellationToken.None);

    Assert.AreEqual("error", actual!.Message);
  }


  // TODO: generated consumer logging tests [without MockLogger]
  // https://github.com/nsubstitute/NSubstitute/issues/597
}