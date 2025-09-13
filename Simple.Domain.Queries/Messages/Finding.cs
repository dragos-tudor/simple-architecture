
namespace Simple.Domain.Queries;

partial class QueriesFuncs
{
  public static TQueryable FindMessageById<TQueryable>(TQueryable query, Guid messageId) where TQueryable : IQueryable<Message> =>
    (TQueryable)query.Where(message => message.MessageId == messageId);

  public static TQueryable FindMessagesByCorrelationId<TQueryable>(TQueryable query, string correlationId) where TQueryable : IQueryable<Message> =>
    (TQueryable)query.Where(message => message.CorrelationId == correlationId);

  public static TQueryable FindMessagesByParentId<TQueryable>(TQueryable query, Guid parentId) where TQueryable : IQueryable<Message> =>
    (TQueryable)query.Where(message => message.ParentId == parentId);

  public static TQueryable FindMessageDuplication<TQueryable>(TQueryable query, MessageIdempotency messageIdempotency) where TQueryable : IQueryable<Message> =>
    (TQueryable)query.Where(message => message.ParentId == messageIdempotency.ParentId && message.MessageType == messageIdempotency.MessageType);

  public static TQueryable FindMessagesPage<TQueryable>(TQueryable query, int? pageIndex, int? pageSize) where TQueryable : IQueryable<Message> =>
    (TQueryable)query.Skip(pageIndex ?? (0 * pageSize) ?? DefaultPageSize).Take(pageSize ?? DefaultPageSize);

  public static TQueryable FindPendingMessages<TQueryable>(TQueryable query, DateTime exclusiveMinDate, DateTime inclusiveMaxDate, int batchSize = 20) where TQueryable : IQueryable<Message> =>
    (TQueryable)query
      .Where(message => message.IsPending)
      .Where(message => exclusiveMinDate < message.MessageDate && message.MessageDate <= inclusiveMaxDate)
      .OrderBy(message => message.MessageDate)
      .Take(batchSize);
}