
namespace Simple.Infrastructure.Mediator;

partial class MediatorTests
{
  [TestMethod]
  public void registered_subscriber__unregister_subscriber__subscriber_unregistered()
  {
    var sub = CreateSubscriber<string>("sub", (_, _) => Task.FromResult(Empty));
    var subs = RegisterSubscriber(sub, []);

    var actual = UnregisterSubscriber("sub", FromSuccess(subs)!);
    AreEqual(actual, []);
  }

  [TestMethod]
  public void registered_subscribers_on_same_message_type__unregister_subscriber__only_subscriber_unregistered()
  {
    var sub1 = CreateSubscriber<string>("sub1", (_, _) => Task.FromResult(Empty));
    var subs1 = RegisterSubscriber(sub1, []);

    var sub2 = CreateSubscriber<string>("sub2", (_, _) => Task.FromResult(Empty));
    var subs2 = RegisterSubscriber(sub2, FromSuccess(subs1)!);

    var actual = UnregisterSubscriber("sub1", FromSuccess(subs2)!);
    AreEqual(actual, [sub2]);
  }

  [TestMethod]
  public void registered_subscribers_on_different_message_type__unregister_subscriber__only_subscriber_unregistered()
  {
    var sub1 = CreateSubscriber<string>("sub1", (_, _) => Task.FromResult(Empty));
    var subs1 = RegisterSubscriber(sub1, []);

    var sub2 = CreateSubscriber<int>("sub2", (_, _) => Task.FromResult(Empty));
    var subs2 = RegisterSubscriber(sub2, FromSuccess(subs1)!);

    var actual = UnregisterSubscriber("sub1", FromSuccess(subs2)!);
    AreEqual(actual, [sub2]);
  }


}