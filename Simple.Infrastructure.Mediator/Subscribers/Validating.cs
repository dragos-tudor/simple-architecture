
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  static string? ValidateSubscriberDuplicate<TMessage> (IEnumerable<Subscriber<TMessage>> subscribers, Subscriber<TMessage> subscriber) =>
    IsDuplicateSubscriber(subscribers, subscriber)? GetDuplicateSubscriberError(subscriber): default;

  static string? ValidateSubscriberMessageType (string messageType) =>
    IsMissingSubscriberMessageType(messageType)? GetMissingSubscriberMessageTypeError(): default;

  static string? ValidateSubscriberId (string subscriberId) =>
    IsMissingSubscriberId(subscriberId)? GetMissingSubscriberIdError(): default;

  static IEnumerable<string?> ValidateSubscriber<TMessage> (IEnumerable<Subscriber<TMessage>> subscribers, Subscriber<TMessage> subscriber) => [
    ValidateSubscriberDuplicate(subscribers, subscriber),
    ValidateSubscriberId(subscriber.SubscriberId),
    ValidateSubscriberMessageType(subscriber.MessageType)
  ];

}