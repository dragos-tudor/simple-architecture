
using static System.Threading.Tasks.Task;

namespace Simple.Infrastructure.Mediator;

partial class MediatorTests
{
  [TestMethod]
  public async Task subscriber_with_message_handler__publish_message__handler_result()
  {
    var sub = CreateSubscriber<string>("sub", (msg, _) => FromResult((string?)(msg == "1"? "one": "not one")));

    var results = PublishMessage("1", [sub]);
    CollectionAssert.AreEqual(await WhenAll(results), AsArray(["one"]));
  }

  [TestMethod]
  public async Task subscribers_with_message_handlers__publish_message__handlers_results()
  {
    var sub1 = CreateSubscriber<string>("sub1", (msg, _) => FromResult((string?)(msg == "1"? "one": "not one")));
    var sub2 = CreateSubscriber<string>("sub2", (msg, _) => FromResult((string?)(msg == "2"? "two": "not two")));

    var results = PublishMessage("1", [sub1, sub2]);
    CollectionAssert.AreEqual(await WhenAll(results), AsArray(["one", "not two"]));
  }

  [TestMethod]
  public async Task subscribers_with_long_running_handlers__publish_message_with_cancellation_request__some_handlers_skipped()
  {
    using var cts = new CancellationTokenSource();
    var sub1 = CreateSubscriber<int>("sub1", async (_, _) => { await cts.CancelAsync(); return "ran"; });
    var sub2 = CreateSubscriber<int>("sub2", (_, token) => token.IsCancellationRequested? FromResult((string?)"not ran"): FromResult((string?)"ran"));

    var results = PublishMessage(1, [sub1, sub2], cts.Token);
    CollectionAssert.AreEqual(await WhenAll(results), AsArray(["ran", "not ran"]));
  }

  [TestMethod]
  public async Task subscriber_with_error_throwing_message_handler__publish_message__handler_exception()
  {
    var sub = CreateSubscriber<string>("sub", (msg, _) => { throw new ArgumentException(msg); });

    var results = PublishMessage("error", [sub]);
    await Assert.ThrowsExceptionAsync<ArgumentException>(() => WhenAll(results), "error");
  }

}