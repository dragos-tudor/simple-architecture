
namespace Simple.Domain.Models;

public record Notification (string From , string To, string Title, string Content, DateTimeOffset Date);

public record AddedToAgendaNotification(string From , string To, string Title, string Content, DateTimeOffset Date): Notification(From, To, Title, Content, Date);