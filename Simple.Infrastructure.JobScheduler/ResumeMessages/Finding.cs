
namespace Simple.Infrastructure.JobScheduler;

partial class JobSchedulerFuncs
{
  static TQueryable FindResumableMessages<TQueryable> (TQueryable query, DateTime exclusiveMinDate, DateTime inclusiveMaxDate, int batchSize) where TQueryable: IQueryable<Message> =>
    (TQueryable)FindActiveMessages(query, exclusiveMinDate, inclusiveMaxDate).Take(batchSize);
}