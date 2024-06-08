
using static System.Threading.Tasks.Task;

namespace Simple.Infrastructure.Mediator;

partial class MediatorTests
{
  [TestMethod]
  public async Task subscriber_with_message_handler__publish_message__handler_result()
  {
    var sub = CreateSubscriber<string, string, string>("sub", (msg, _) => FromResult(msg == "1"? "one": "not one") as Task<string?>);

    var results = await PublishMessage<string, string, string>("1", [sub]);
    CollectionAssert.AreEqual(results.Select(e => e).ToArray(), ToArray(["one"]));
  }

  [TestMethod]
  public async Task subscribers_with_message_handlers__publish_message__handlers_results()
  {
    var sub1 = CreateSubscriber<string, string, string>("sub1", (msg, _) => FromResult(msg == "1"? "one": "not one")  as Task<string?>);
    var sub2 = CreateSubscriber<string, string, string>("sub2", (msg, _) => FromResult(msg == "2"? "two": "not two")  as Task<string?>);

    var results = await PublishMessage<string, string, string>("1", [sub1, sub2]);
    CollectionAssert.AreEqual(results.Select(e => e).ToArray(), ToArray(["one", "not two"]));
  }

  [TestMethod]
  public async Task subscribers_with_long_running_handlers__publish_message_with_cancellation_request__some_handlers_skipped()
  {
    using var cts = new CancellationTokenSource();
    var sub1 = CreateSubscriber<int, string, string>("sub1", async (_, _) => { await cts.CancelAsync(); return "ran"; });
    var sub2 = CreateSubscriber<int, string, string>("sub2", (_, token) => FromResult(cts.IsCancellationRequested ? "not ran": "ran") as Task<string?>);

    var results = await PublishMessage<int, string, string>(1, [sub1, sub2], cts.Token);
    CollectionAssert.AreEqual(results.Select(e => e).ToArray(), ToArray(["ran", "not ran"]));
  }

  [TestMethod]
  public async Task subscriber_with_error_throwing_message_handler__publish_message__handler_exception()
  {
    var sub = CreateSubscriber<string, string, string>("sub", (msg, _) => { throw new ArgumentException(msg); });

    var results = PublishMessage<string, string, string>("error", [sub]);
    await Assert.ThrowsExceptionAsync<ArgumentException>(() => results, "error");
  }

}