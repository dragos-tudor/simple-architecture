
namespace Simple.Infrastructure.Mediator;

partial class MediatorTests
{
  [TestMethod]
  public void new_subscriber__register_subscriber__subscriber_registered()
  {
    var sub = CreateSubscriber<string>("sub", (_, _) => Task.FromResult(default(string)));
    var subs = RegisterSubscriber(sub, []);

    var actual = FindSubscribers(FromSuccess(subs)!, typeof(string).Name);
    AreEqual(actual, [sub]);
  }

  [TestMethod]
  public void new_subscribers_with_same_message_type__register_subscribers__subscribers_registered()
  {
    var sub1 = CreateSubscriber<string>("sub1", (_, _) => Task.FromResult(default(string)));
    var sub2 = CreateSubscriber<string>("sub2", (_, _) => Task.FromResult(default(string)));
    var subs2 = RegisterSubscriber(sub2, [sub1]);

    var actual = FindSubscribers(FromSuccess(subs2)!, typeof(string).Name);
    AreEqual(actual, [sub1, sub2]);
  }

  [TestMethod]
  public void new_subscribers_with_different_message_types__register_subscribers__subscribers_registered()
  {
    var sub1 = CreateSubscriber<string, string>("sub1", (_, _) => Task.FromResult(default(string)));
    var sub2 = CreateSubscriber<string, int>("sub2", (_, _) => Task.FromResult(default(string)));
    var subs2 = RegisterSubscriber(sub2, [sub1]);

    var actual = FindSubscribers(FromSuccess(subs2)!, typeof(int).Name);
    AreEqual(actual, [sub2]);
  }

  [TestMethod]
  public void registered_subscriber__register_new_subscriber_with_same_id__duplicate_subscriber_error()
  {
    var sub1 = CreateSubscriber<string>("sub1", (_, _) => Task.FromResult(default(string)));
    var sub2 = CreateSubscriber<string>("sub1", (_, _) => Task.FromResult(default(string)));
    var actual = RegisterSubscriber(sub2, [sub1]);

    AreEqual(FromFailure(actual)!, ["Duplicate subscriber sub1."]);
  }

  [TestMethod]
  public void new_subscriber_wihout_id__register_subscriber__missing_subscriber_id_error()
  {
    var actual = RegisterSubscriber(new Subscriber<string>(default!, "some type", default!), []);
    AreEqual(FromFailure(actual)!, ["Missing subscriber id."]);
  }

  [TestMethod]
  public void new_subscriber_wihout_message_type__register_subscriber__missing_message_type_error()
  {
    var actual = RegisterSubscriber(new Subscriber<string>("sub1", default!, default!), []);
    AreEqual(FromFailure(actual)!, ["Missing subscriber message type."]);
  }

}