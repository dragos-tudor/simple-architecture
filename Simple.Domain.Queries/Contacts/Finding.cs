
namespace Simple.Domain.Queries;

partial class QueriesFuncs
{
  const int DefaultPageSize = 10;

  public static TQueryable FindContactByEmail<TQueryable>(TQueryable query, string contactEmail) where TQueryable : IQueryable<Contact> =>
    (TQueryable)query.Where(contact => contact.ContactEmail == contactEmail);

  public static TQueryable FindContactById<TQueryable>(TQueryable query, Guid contactId) where TQueryable : IQueryable<Contact> =>
    (TQueryable)query.Where(contact => contact.ContactId == contactId);

  public static TQueryable FindContactByName<TQueryable>(TQueryable query, string contactName) where TQueryable : IQueryable<Contact> =>
    (TQueryable)query.Where(contact => contact.ContactName == contactName);

  public static TQueryable FindContactsPage<TQueryable>(TQueryable query, int? pageIndex, int? pageSize) where TQueryable : IQueryable<Contact> =>
    (TQueryable)query.Skip((pageIndex ?? 0) * (pageSize ?? DefaultPageSize)).Take(pageSize ?? DefaultPageSize);
}