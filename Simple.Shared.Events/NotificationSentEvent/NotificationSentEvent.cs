
namespace Simple.Shared.Events;

public record NotificationSentEvent(string From, string To, string Subject, string Content, DateTime Date) : IEvent;