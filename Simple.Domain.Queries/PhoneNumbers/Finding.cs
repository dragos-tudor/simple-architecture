
namespace Simple.Domain.Queries;

partial class QueriesFuncs
{
  public static TQueryable FindPhoneNumber<TQueryable>(TQueryable query, PhoneNumber phoneNumber) where TQueryable : IQueryable<PhoneNumber> =>
    (TQueryable)query.Where(_phoneNumber => _phoneNumber.CountryCode == phoneNumber.CountryCode && _phoneNumber.Number == phoneNumber.Number);

  public static TQueryable FindContactPhoneNumber<TQueryable>(TQueryable query, PhoneNumber phoneNumber) where TQueryable : IQueryable<Contact> =>
    (TQueryable)query.Where(contact => contact.PhoneNumbers.Any(_phoneNumber => _phoneNumber.CountryCode == phoneNumber.CountryCode && _phoneNumber.Number == phoneNumber.Number));
}