
namespace Simple.Shared.Events;

public record ContactCreatedEvent(Guid ContactId, string ContactEmail) : IEvent;
