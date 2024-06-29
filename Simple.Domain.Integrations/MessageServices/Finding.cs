
namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  static TQueryable FindResumableMessages<TQueryable> (TQueryable query, DateTime minDate, DateTime maxDate, int batchSize) where TQueryable: IQueryable<Message> =>
    (TQueryable)FindActiveMessages(query, minDate, maxDate).Take(batchSize);
}