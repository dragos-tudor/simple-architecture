
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  static string GetDuplicateSubscriberError<TMessage> (Subscriber<TMessage> subscriber) => $"Duplicate subscriber {subscriber.SubscriberId}.";

  static string GetMissingSubscriberMessageTypeError () => $"Missing subscriber message type.";

  static string GetMissingSubscriberIdError () => $"Missing subscriber id.";
}