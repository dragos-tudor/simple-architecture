
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static TQueryable FindActiveMessages<TQueryable> (TQueryable query, DateTime maxDate) where TQueryable: IQueryable<Message> =>
    (TQueryable)query.Where(message => message.IsActive && message.MessageDate < maxDate);

  public static TQueryable FindActiveMessages<TQueryable> (TQueryable query, DateTime maxDate, TimeSpan maxDateDelay) where TQueryable: IQueryable<Message> =>
    FindActiveMessages(query, GetMessageDateDelay(maxDate, maxDateDelay));

  public static TQueryable FindMessageByKey<TQueryable> (TQueryable query, Guid messageId) where TQueryable: IQueryable<Message> =>
    (TQueryable)query.Where(message => message.MessageId == messageId);

  public static TQueryable FindMessageDuplication<TQueryable> (TQueryable query, MessageIdempotency messageIdempotency) where TQueryable: IQueryable<Message> =>
    (TQueryable)query.Where(message => message.ParentId == messageIdempotency.ParentId && message.MessageType == messageIdempotency.MessageType);

  public static TQueryable FindMessagesPage<TQueryable> (TQueryable query, int? pageIndex, int? pageSize) where TQueryable: IQueryable<Message> =>
    (TQueryable)query.Skip(pageIndex ?? 0 * pageSize ?? DefaultPageSize).Take(pageSize ?? DefaultPageSize);
}