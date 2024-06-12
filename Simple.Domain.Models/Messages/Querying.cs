
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static TQueryable FindActiveMessages<TQueryable> (TQueryable query) where TQueryable: IQueryable<Message> =>
    (TQueryable)query.Where(message => message.IsActive);

  public static TQueryable FindMessageByKey<TQueryable> (TQueryable query, Guid messageId) where TQueryable: IQueryable<Message> =>
    (TQueryable)query.Where(message => message.MessageId == messageId);

  public static TQueryable FindMessageByParent<TQueryable> (TQueryable query, Guid parentId) where TQueryable: IQueryable<Message> =>
    (TQueryable)query.Where(message => message.ParentId == parentId);
}