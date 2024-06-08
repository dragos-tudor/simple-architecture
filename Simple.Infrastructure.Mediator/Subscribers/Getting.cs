
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  static string GetDuplicateSubscriberError<TMessage, TFailure> (Subscriber<TMessage, TFailure> subscriber) => $"Duplicate subscriber {subscriber.SubscriberId}.";

  static string GetMissingSubscriberIdError () => $"Missing subscriber id.";

  static string GetMissingSubscriberMessageTypeError () => $"Missing subscriber message type.";
}