
namespace Simple.Domain.Services;

public delegate Task<IEnumerable<string>> FindPhoneNumbers (IEnumerable<PhoneNumber> phoneNumbers);

public delegate Task SaveModelAndMessage<TModel, TPayload> (TModel model, Message<TPayload> message);

public delegate Task PublishMessage<TPayload> (Message<TPayload> message);