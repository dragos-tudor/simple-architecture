
using static System.Threading.Tasks.Task;

namespace Simple.Infrastructure.Mediator;

partial class MediatorTests
{
  [TestMethod]
  public async Task subscriber_with_message_handler__publish_message__message_handled()
  {
    var actual = string.Empty;
    var sub = CreateSubscriber<string, string>("sub", async (msg, _) => { await CompletedTask; actual = msg; });

    await PublishMessage<string, string>("1", [sub]);
    Assert.AreEqual(actual, "1");
  }

  [TestMethod]
  public async Task subscribers_with_message_handlers__publish_message__message_handled()
  {
    var actual = new List<string>();
    var sub1 = CreateSubscriber<string, string>("sub1", async (msg, _) => { await CompletedTask; actual.Add("sub1"); });
    var sub2 = CreateSubscriber<string, string>("sub2", async (msg, _) => { await CompletedTask; actual.Add("sub2"); });

    await PublishMessage<string, string>("1", [sub1, sub2]);
    AreEqual(actual, ["sub1", "sub2"]);
  }

  [TestMethod]
  public async Task subscribers_with_long_running_handlers__publish_message_with_cancellation_request__some_message_handlers_skipped()
  {
    using var cts = new CancellationTokenSource();
    var actual = new List<string>();
    var sub1 = CreateSubscriber<int, string>("sub1", async (_, _) => { await cts.CancelAsync(); actual.Add("ran"); });
    var sub2 = CreateSubscriber<int, string>("sub2", async (_, token) => { await CompletedTask; actual.Add(cts.IsCancellationRequested ? "not ran": "ran"); });

    await PublishMessage<int, string>(1, [sub1, sub2], cts.Token);
    AreEqual(actual, ["ran", "not ran"]);
  }

  [TestMethod]
  public async Task subscriber_with_error_throwing_message_handler__publish_message__handler_exception()
  {
    var sub = CreateSubscriber<string, string>("sub", (msg, _) => { throw new ArgumentException(msg); });

    await Assert.ThrowsExceptionAsync<ArgumentException>(() => PublishMessage<string, string>("error", [sub]), "error");
  }
}