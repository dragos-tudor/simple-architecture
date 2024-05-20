using System.Globalization;
using static System.Threading.Tasks.Task;
#pragma warning disable CA5394

namespace Simple.Infrastructure.Mediator;

partial class MediatorTests
{
  [TestMethod]
  public async Task mediator_subscribers__mediator_publish_message__handlers_results()
  {
    var mediator = CreateMediator();
    mediator.Subscribe<int>("sub1", (msg, _) => FromResult(ToString(msg.MessagePayload)));
    mediator.Subscribe<int>("sub2", (msg, _) => FromResult(ToString(msg.MessagePayload + 1)));

    var results = mediator.Publish(CreateMessage(1));
    AreEqual(await WhenAll(results), ["1", "2"]);
  }

  [TestMethod]
  public async Task thread_safe_mediator_subscribers__mediator_publish_message__handlers_results()
  {
    using var mediator = CreateThreadSafeMediator();
    mediator.Subscribe<int>("sub1", (msg, _) => FromResult(ToString(msg.MessagePayload)));
    mediator.Subscribe<int>("sub2", (msg, _) => FromResult(ToString(msg.MessagePayload + 1)));

    var results = mediator.Publish(CreateMessage(1));
    AreEqual(await WhenAll(results), ["1", "2"]);
  }

  [TestMethod]
  [DataRow(10000, 8)]
  [DataRow(20000, 5)]
  [DataRow(30000, 3)]
  public async Task thread_safe_mediator__randomly_subscribe_publish_unsubscribe__no_errors(int iterations, int randomMax)
  {
    using var mediator = CreateThreadSafeMediator();
    var random = Random.Shared;

    Func<Task>[] funcs = [
      () => FromResult(mediator.Subscribe<int>(ToString(random.Next(randomMax)), (msg, _) => FromResult(Empty) )),
      () => FromResult(mediator.Unsubscribe<int>(ToString(random.Next(randomMax)))),
      () => WhenAll(mediator.Publish(CreateMessage(random.Next(randomMax))))
    ];

    await Parallel.ForAsync(0, iterations, async (_, _) => {
      try { await funcs[random.Next(0, 3)](); }
      catch(Exception ex) { Assert.Fail(ex.Message); }
    });
  }

  static string ToString(int number) => number.ToString(CultureInfo.InvariantCulture);
}