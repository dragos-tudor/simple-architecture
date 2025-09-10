#pragma warning disable CS4014

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Time.Testing;

namespace Simple.Infrastructure.JobScheduler;

partial class JobSchedulerTests
{
  static readonly ILogger Logger = new NullLoggerFactory().CreateLogger("");

  [TestMethod]
  public void enqueue_messages_from_different_threads__process_messages__messages_processed()
  {
    var timeProvider = new FakeTimeProvider(DateTime.UtcNow);
    Message[] messages = [
      CreateTestMessage(isPending: true, messageDate: DateTime.UtcNow.AddHours(-1)),
      CreateTestMessage(isPending: true, messageDate: DateTime.UtcNow.AddHours(-2)),
      CreateTestMessage(isPending: true, messageDate: DateTime.UtcNow.AddHours(-3))
    ];
    var jobOptions = new MessageJobOptions() { BatchSize = messages.Count() - 1 };
    using var counter = new CountdownEvent(messages.Count());

    ProcessMessage<Message> processMessage = (message, _) => { message.IsPending = false; return Task.FromResult(counter.Signal()); };
    HandleMessageError<Message> handleMessageError = (_, __, ___) => Task.CompletedTask;
    QueryMessages<Message> findMessages = (minDate, maxDate, batchSize, _) => Task.FromResult(QueryPendingMessages(messages.AsQueryable(), minDate, maxDate, batchSize).ToList());

    ProcessMessagesAsync(processMessage, handleMessageError, findMessages, jobOptions, timeProvider, Logger, CancellationToken.None);

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
}