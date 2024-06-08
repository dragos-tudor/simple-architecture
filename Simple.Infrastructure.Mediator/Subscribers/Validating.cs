
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  static string? ValidateSubscriberDuplicate<TMessage, TFailure> (IEnumerable<Subscriber<TMessage, TFailure>> subscribers, Subscriber<TMessage, TFailure> subscriber) =>
    IsDuplicateSubscriber(subscribers, subscriber)? GetDuplicateSubscriberError(subscriber): default;

  static string? ValidateSubscriberMessageType (string messageType) =>
    IsMissingSubscriberMessageType(messageType)? GetMissingSubscriberMessageTypeError(): default;

  static string? ValidateSubscriberId (string subscriberId) =>
    IsMissingSubscriberId(subscriberId)? GetMissingSubscriberIdError(): default;

  static IEnumerable<string?> ValidateSubscriber<TMessage, TFailure> (IEnumerable<Subscriber<TMessage, TFailure>> subscribers, Subscriber<TMessage, TFailure> subscriber) => [
    ValidateSubscriberDuplicate(subscribers, subscriber),
    ValidateSubscriberId(subscriber.SubscriberId),
    ValidateSubscriberMessageType(subscriber.MessageType)
  ];

}