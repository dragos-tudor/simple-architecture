
using static System.Threading.Tasks.Task;

namespace Simple.Infrastructure.Mediator;

partial class MediatorTests
{
  [TestMethod]
  public async Task subscriber_with_message_handler__publish_message__handler_result()
  {
    var sub = CreateSubscriber<int>("sub", (msg, _) => FromResult(msg.MessagePayload == 1? "one": "not one"));
    var subs = RegisterSubscriber(sub, []);

    var results = PublishMessage(CreateMessage(1), FromSuccess(subs)!);
    AreEqual(await WhenAll(results), ["one"]);
  }

  [TestMethod]
  public async Task subscribers_with_message_handlers__publish_message__handlers_results()
  {
    var sub1 = CreateSubscriber<int>("sub1", (msg, _) => FromResult(msg.MessagePayload == 1? "one": "not one"));
    var subs1 = RegisterSubscriber(sub1, []);

    var sub2 = CreateSubscriber<int>("sub2", (msg, _) => FromResult(msg.MessagePayload == 2? "two": "not two"));
    var subs2 = RegisterSubscriber(sub2, FromSuccess(subs1)!);

    var results = PublishMessage(CreateMessage(1), FromSuccess(subs2)!);
    AreEqual(await WhenAll(results), ["one", "not two"]);
  }


  [TestMethod]
  public async Task subscribers_with_long_running_handlers__publish_message_with_cancellation_request__some_handlers_skipped()
  {
    using var cts = new CancellationTokenSource();
    var sub1 = CreateSubscriber<int>("sub1", async (_, _) => { await cts.CancelAsync(); return "ran"; });
    var subs1 = RegisterSubscriber(sub1, []);

    var sub2 = CreateSubscriber<int>("sub2", (_, token) => token.IsCancellationRequested? FromResult("not ran"): FromResult("ran"));
    var subs2 = RegisterSubscriber(sub2, FromSuccess(subs1)!);

    var results = PublishMessage(CreateMessage(1), FromSuccess(subs2)!, cts.Token);

    AreEqual(await WhenAll(results), ["ran", "not ran"]);
  }

  [TestMethod]
  public async Task subscriber_with_error_throwing_message_handler__publish_message__handler_exception()
  {
    var sub = CreateSubscriber<string>("sub", (msg, _) => { throw new ArgumentException(msg.MessagePayload); });
    var subs = RegisterSubscriber(sub, []);

    var results = PublishMessage(CreateMessage("1"), FromSuccess(subs)!);
    await Assert.ThrowsExceptionAsync<ArgumentException>(() => WhenAll(results), "error");
  }

}