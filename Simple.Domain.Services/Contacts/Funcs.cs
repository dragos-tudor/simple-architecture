
namespace Simple.Domain.Services;

public delegate Task<IEnumerable<string>> FindPhoneNumbers (IEnumerable<PhoneNumber> phoneNumbers);

public delegate Task SaveModelAndEvent<TModel, TEvent> (TModel model, TEvent @event);

public delegate Task PublishEvent<TEvent> (TEvent @event);