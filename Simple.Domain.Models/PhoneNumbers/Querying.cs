
namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static TQueryable FindPhoneNumber<TQueryable> (TQueryable query, PhoneNumber phoneNumber) where TQueryable: IQueryable<PhoneNumber> =>
    (TQueryable)query.Where(_phoneNumber => _phoneNumber.CountryCode == phoneNumber.CountryCode && _phoneNumber.Number == phoneNumber.Number );

  public static TQueryable FindPhoneNumbers<TQueryable> (TQueryable query, IEnumerable<long> numbers) where TQueryable: IQueryable<PhoneNumber> =>
    (TQueryable)query.Where(phoneNumber => numbers.Contains(phoneNumber.Number));
}