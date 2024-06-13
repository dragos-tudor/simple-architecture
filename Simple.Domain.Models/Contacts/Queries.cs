
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  const int DefaultPageSize = 10;

  public static TQueryable FindContactByEmail<TQueryable> (TQueryable query, string contactEmail) where TQueryable: IQueryable<Contact> =>
    (TQueryable)query.Where(contact => contact.ContactEmail == contactEmail);

  public static TQueryable FindContactByKey<TQueryable> (TQueryable query, Guid contactId) where TQueryable: IQueryable<Contact> =>
    (TQueryable)query.Where(contact => contact.ContactId == contactId);

  public static TQueryable FindContactByName<TQueryable> (TQueryable query, string contactName) where TQueryable: IQueryable<Contact> =>
    (TQueryable)query.Where(contact => contact.ContactName == contactName);

  public static TQueryable FindContactByPhoneNumber<TQueryable> (TQueryable query, PhoneNumber phoneNumber) where TQueryable: IQueryable<Contact> =>
    (TQueryable)query.Where(contact => contact.PhoneNumbers.Any(_phoneNumber => _phoneNumber == phoneNumber));

  public static TQueryable GetContactsPage<TQueryable> (TQueryable query, int? pageIndex, int? pageSize) where TQueryable: IQueryable<Contact> =>
    (TQueryable)query.Skip(pageIndex ?? 0 * pageSize ?? DefaultPageSize).Take(pageSize ?? DefaultPageSize);
}