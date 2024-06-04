
namespace Simple.Infrastructure.Mediator;

partial class MediatorTests
{
  [TestMethod]
  public void registered_subscriber__unregister_subscriber__subscriber_unregistered()
  {
    var sub = CreateSubscriber<string>("sub", (_, _) => Task.FromResult(default(string)));
    var subs = RegisterSubscriber(sub, []);

    var actual = UnregisterSubscriber("sub", FromSuccess(subs)!);
    CollectionAssert.AreEqual(actual, AsArray<string>([]));
  }

  [TestMethod]
  public void registered_subscribers_on_same_message_type__unregister_one_subscriber__that_subscriber_unregistered()
  {
    var sub1 = CreateSubscriber<string>("sub1", (_, _) => Task.FromResult(default(string)));
    var sub2 = CreateSubscriber<string>("sub2", (_, _) => Task.FromResult(default(string)));
    var subs2 = RegisterSubscriber(sub2, [sub1]);

    var actual = UnregisterSubscriber("sub1", FromSuccess(subs2)!);
    CollectionAssert.AreEqual(actual, AsArray([sub2]));
  }

  [TestMethod]
  public void registered_subscribers_with_different_message_type__unregister_one_subscriber_with_one_message_type__that_subscriber_unregistered()
  {
    var sub1 = CreateSubscriber<string, string>("sub1", (_, _) => Task.FromResult(default(string)));
    var sub2 = CreateSubscriber<string, int>("sub2", (_, _) => Task.FromResult(default(string)));
    var subs2 = RegisterSubscriber(sub2, [sub1]);

    var actual = UnregisterSubscriber("sub1", FromSuccess(subs2)!);
    CollectionAssert.AreEqual(actual, AsArray([sub2]));
  }


}