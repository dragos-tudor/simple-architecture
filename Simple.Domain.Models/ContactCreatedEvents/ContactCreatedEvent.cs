
namespace Simple.Domain.Models;

public record ContactCreatedEvent(Guid ContactId, string ContactEmail) : IEvent;
