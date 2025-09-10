
namespace Simple.Domain.Queries;

partial class QueriesFuncs
{
  public static TQueryable QueryPendingMessages<TQueryable>(TQueryable query, DateTime exclusiveMinDate, DateTime inclusiveMaxDate, int batchSize = 20) where TQueryable : IQueryable<Message> =>
    (TQueryable)query
      .Where(message => message.IsPending)
      .Where(message => exclusiveMinDate < message.MessageDate && message.MessageDate <= inclusiveMaxDate)
      .OrderBy(message => message.MessageDate)
      .Take(batchSize);
}