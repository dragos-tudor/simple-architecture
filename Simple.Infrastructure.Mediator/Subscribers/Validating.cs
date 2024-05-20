
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  static string? ValidateSubscriberDuplicate<TMessage> (IEnumerable<Subscriber<TMessage>> subscribers, Subscriber<TMessage> subscriber) =>
    !IsDuplicateSubscriber(subscribers, subscriber)? default: GetDuplicateSubscriberError(subscriber);

  static string? ValidateSubscriberMessageType (string messageType) =>
    IsMissingSubscriberMessageType(messageType)? default: GetMissingSubscriberMessageTypeError();

  static string? ValidateSubscriberId (string subscriberId) =>
    IsMissingSubscriberId(subscriberId)? default: GetMissingSubscriberIdError();

  static IEnumerable<string> ValidateSubscriber<TMessage> (Subscriber<TMessage> subscriber) =>
    GetValidationErrors([
      ValidateSubscriberId(subscriber.SubscriberId),
      ValidateSubscriberMessageType(subscriber.MessageType)
    ]);

  static IEnumerable<string> ValidateSubscriber<TMessage> (IEnumerable<Subscriber<TMessage>> subscribers, Subscriber<TMessage> subscriber) =>
    GetValidationErrors([
      ValidateSubscriberDuplicate(subscribers, subscriber),
      ValidateSubscriberId(subscriber.SubscriberId),
      ValidateSubscriberMessageType(subscriber.MessageType)
    ]);

}