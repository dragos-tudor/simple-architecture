
using static System.Threading.Tasks.Task;

namespace Simple.Infrastructure.Mediator;

partial class MediatorTests
{
  [TestMethod]
  public async Task some_subscribers_with_async_error_throwing_message_handlers__handle_message_in_parralel__no_message_handlers_skipped()
  {
    var actual = new List<string>();
    var sub1 = CreateSubscriber<string, string>("sub1", async (msg, _) => { await CompletedTask; throw new ArgumentException(msg); });
    var sub2 = CreateSubscriber<string, string>("sub2", async (msg, _) => { await CompletedTask; actual.Add("sub2"); });

    await Assert.ThrowsExceptionAsync<ArgumentException>(() => HandleMessageParallel("error", typeof(string).Name, [sub1, sub2]), "error");
    AreEqual(actual, ["sub2"]);
  }

  [TestMethod]
  public async Task some_subscribers_with_direct_error_throwing_message_handlers__handle_message_in_parralel__after_error_throwing_message_handlers_skipped()
  {
    var actual = new List<string>();
    var sub1 = CreateSubscriber<string, string>("sub1", (msg, _) => { throw new ArgumentException(msg); });
    var sub2 = CreateSubscriber<string, string>("sub2", async (msg, _) => { await CompletedTask; actual.Add("sub2"); });

    await Assert.ThrowsExceptionAsync<ArgumentException>(() => HandleMessageParallel("error", typeof(string).Name, [sub1, sub2]), "error");
    AreEqual(actual, []);
  }

  [TestMethod]
  public async Task some_subscribers_with_async_error_throwing_message_handlers__handle_message_in_serial__after_error_throwing_message_handlers_skipped()
  {
    var actual = new List<string>();
    var sub1 = CreateSubscriber<string, string>("sub1", async (msg, _) => { await CompletedTask; throw new ArgumentException(msg); });
    var sub2 = CreateSubscriber<string, string>("sub2", async (msg, _) => { await CompletedTask; actual.Add("sub2"); });

    await Assert.ThrowsExceptionAsync<ArgumentException>(() => HandleMessageSerial("error", typeof(string).Name, [sub1, sub2]), "error");
    AreEqual(actual, []);
  }

  [TestMethod]
  public async Task some_subscribers_with_direct_error_throwing_message_handlers__handle_message_in_serial__after_error_throwing_message_handlers_skipped()
  {
    var actual = new List<string>();
    var sub1 = CreateSubscriber<string, string>("sub1", (msg, _) => { throw new ArgumentException(msg); });
    var sub2 = CreateSubscriber<string, string>("sub2", async (msg, _) => { await CompletedTask; actual.Add("sub2"); });

    await Assert.ThrowsExceptionAsync<ArgumentException>(() => HandleMessageSerial("error", typeof(string).Name, [sub1, sub2]), "error");
    AreEqual(actual, []);
  }
}