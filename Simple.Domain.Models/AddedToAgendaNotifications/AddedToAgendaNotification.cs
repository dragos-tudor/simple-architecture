
namespace Simple.Domain.Models;

public record AddedToAgendaNotification(string From, string To, string Title, string Content, DateTimeOffset Date) : INotification;