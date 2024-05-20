
namespace Simple.Infrastructure.Mediator;

partial class MediatorTests
{
  [TestMethod]
  public void new_subscriber__register_subscriber__subscriber_registered()
  {
    var sub = CreateSubscriber<string>("sub", (_, _) => Task.FromResult(Empty));
    var subs = RegisterSubscriber(sub, []);

    var actual = FindSubscribers<string>(FromSuccess(subs)!);
    AreEqual(actual, [sub]);
  }

  [TestMethod]
  public void new_subscribers_for_same_message_type__register_subscribers__subscribers_registered()
  {
    var sub1 = CreateSubscriber<string>("sub1", (_, _) => Task.FromResult(Empty));
    var subs1 = RegisterSubscriber(sub1, []);

    var sub2 = CreateSubscriber<string>("sub2", (_, _) => Task.FromResult(Empty));
    var subs2 = RegisterSubscriber(sub2, FromSuccess(subs1)!);

    var actual = FindSubscribers<string>(FromSuccess(subs2)!);
    AreEqual(actual, [sub1, sub2]);
  }

  [TestMethod]
  public void new_subscribers_for_different_message_type__register_subscribers__subscribers_registered()
  {
    var sub1 = CreateSubscriber<string>("sub1", (_, _) => Task.FromResult(Empty));
    var subs1 = RegisterSubscriber(sub1, []);

    var sub2 = CreateSubscriber<int>("sub2", (_, _) => Task.FromResult(Empty));
    var subs2 = RegisterSubscriber(sub2, FromSuccess(subs1)!);

    var actual = FindSubscribers<int>(FromSuccess(subs2)!);
    AreEqual(actual, [sub2]);
  }

  [TestMethod]
  public void registered_subscriber__register_new_subscriber_with_same_id__duplicate_subscriber_error()
  {
    var sub1 = CreateSubscriber<string>("sub1", (_, _) => Task.FromResult(Empty));
    var subs = RegisterSubscriber(sub1, []);

    var sub2 = CreateSubscriber<string>("sub1", (_, _) => Task.FromResult(Empty));
    var actual = RegisterSubscriber(sub2, FromSuccess(subs)!);
    AreEqual(FromFailure(actual)!, ["Duplicate subscriber sub1."]);
  }

  [TestMethod]
  public void new_subscriber_wihout_id__register_subscriber__missing_subscriber_id_error()
  {
    var sub = CreateSubscriber<string>("", (_, _) => Task.FromResult(Empty));

    var actual = RegisterSubscriber(sub, []);
    AreEqual(FromFailure(actual)!, ["Missing subscriber id."]);
  }

  [TestMethod]
  public void new_subscriber_wihout_message_type__register_subscriber__missing_message_type_error()
  {
    var sub = CreateSubscriber<string>("sub", "", (_, _) => Task.FromResult(Empty));

    var actual = RegisterSubscriber(sub, []);
    AreEqual(FromFailure(actual)!, ["Missing subscriber message type."]);
  }

}