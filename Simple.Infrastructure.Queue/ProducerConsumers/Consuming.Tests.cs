
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Simple.Infrastructure.Queue;

partial class QueueTests
{
  static readonly HandleMessage<Message, string> HandleMessage = Substitute.For<HandleMessage<Message, string>>();
  static readonly FinalizeMessage<Message> FinalizeMessage = Substitute.For<FinalizeMessage<Message>>();
  static readonly ILogger Logger = Substitute.For<ILogger>();

  [TestMethod]
  public async Task message__consume_message__message_handled ()
  {
    var queue = CreateMessageQueue<Message>();
    var message = CreateTestMessage();
    using var counter = new CountdownEvent(1); // better solution than Task.Delay.
    HandleMessage<Message, string> handleMessage = Substitute.For<HandleMessage<Message, string>>();

    handleMessage.When(handler => handler(message, default)).Do((_) => counter.Signal());
    _ = ConsumeMessages(queue, handleMessage, FinalizeMessage, Logger);
    ProduceMessage(queue, message);

    counter.Wait();
    await handleMessage.Received().Invoke(message, default);
  }

  [TestMethod]
  public async Task message__consume_message__message_finalized ()
  {
    var queue = CreateMessageQueue<Message>();
    var message = CreateTestMessage();
    using var counter = new CountdownEvent(1);
    FinalizeMessage<Message> finalizeMessage = Substitute.For<FinalizeMessage<Message>>();

    finalizeMessage.When(handler => handler(message, default)).Do((_) => counter.Signal());
    _ = ConsumeMessages(queue, HandleMessage, finalizeMessage, Logger);
    ProduceMessage(queue, message);

    counter.Wait();
    await finalizeMessage.Received().Invoke(message, default);
  }

  [TestMethod]
  public void message_handler_with_failures__consume_message__message_not_finalized ()
  {
    var queue = CreateMessageQueue<Message>();
    var message = CreateTestMessage();
    using var counter = new CountdownEvent(1);
    HandleMessage<Message, string> handleMessage = Substitute.For<HandleMessage<Message, string>>();
    FinalizeMessage<Message> finalizeMessage = Substitute.For<FinalizeMessage<Message>>();

    handleMessage(message, default).Returns(["error"]);
    handleMessage.When(handler => handler(message, default)).Do((_) => counter.Signal());
    _ = ConsumeMessages(queue, handleMessage, finalizeMessage, Logger);
    ProduceMessage(queue, message);

    counter.Wait();
    finalizeMessage.DidNotReceive();
  }

  // TODO: generated consumern logging tests [without MockLogger]
  // https://github.com/nsubstitute/NSubstitute/issues/597
}