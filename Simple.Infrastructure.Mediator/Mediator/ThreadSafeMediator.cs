
namespace Simple.Infrastructure.Mediator;

public sealed class ThreadSafeMediator: Mediator, IDisposable
{
  readonly ReaderWriterLockSlim SubscribersLock = new ();

  public override IEnumerable<Task<string>> Publish<TPayload> (Message<TPayload> message, CancellationToken cancellationToken = default)
  {
    SubscribersLock.EnterReadLock();
    try { return base.Publish(message, cancellationToken); }
    finally { SubscribersLock.ExitReadLock(); }
  }

  public override string[]? Subscribe<TPayload> (string subscriberId, Func<Message<TPayload>, CancellationToken, Task<string>> messageHandler)
  {
    SubscribersLock.EnterWriteLock();
    try { return base.Subscribe(subscriberId, messageHandler); }
    finally { SubscribersLock.ExitWriteLock(); }
  }

  public override bool Unsubscribe<TPayload> (string subscriberId)
  {
    SubscribersLock.EnterWriteLock();
    try { return base.Unsubscribe<TPayload>(subscriberId); }
    finally { SubscribersLock.ExitWriteLock(); }
  }

  public void Dispose() {
    GC.SuppressFinalize(this);
    while(!IsSafeDispose(SubscribersLock))
      continue;
    SubscribersLock.Dispose();
  }

  static bool IsSafeDispose(ReaderWriterLockSlim readerWriterLock) =>
    readerWriterLock.WaitingReadCount == 0 && readerWriterLock.WaitingWriteCount == 0;
}