
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static TQueryable FindActiveMessages<TQueryable> (TQueryable query) where TQueryable: IQueryable<Message> =>
    (TQueryable)query.Where(message => message.IsActive);

  public static TQueryable FindMessageByKey<TQueryable> (TQueryable query, Guid messageId) where TQueryable: IQueryable<Message> =>
    (TQueryable)query.Where(message => message.MessageId == messageId);

  public static TQueryable FindMessageDuplication<TQueryable> (TQueryable query,MessageIdempotency messageIdempotency) where TQueryable: IQueryable<Message> =>
    (TQueryable)query.Where(message => message.ParentId == messageIdempotency.ParentId && message.MessageType == messageIdempotency.MessageType);

  public static TQueryable GetMessagesPage<TQueryable> (TQueryable query, int? pageIndex, int? pageSize) where TQueryable: IQueryable<Message> =>
    (TQueryable)query.Skip(pageIndex ?? 0 * pageSize ?? DefaultPageSize).Take(pageSize ?? DefaultPageSize);
}