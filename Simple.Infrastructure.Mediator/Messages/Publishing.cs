
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  public static IEnumerable<Task<string?>> PublishMessage<TPayload> (
    Message<TPayload> message,
    IEnumerable<Subscriber<Message>> subscribers,
    CancellationToken cancellationToken = default)
  =>
    DispatchMessage(message, GetTypeName<TPayload>(), subscribers, cancellationToken);
}