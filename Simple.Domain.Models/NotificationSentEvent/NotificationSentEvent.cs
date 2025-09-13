
namespace Simple.Domain.Models;

public record NotificationSentEvent(string From, string To, string Subject, string Content, DateTime Date) : IEvent;