
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  static PhoneNumber AddContactPhoneNumber (AgendaContext dbContext, Contact contact, PhoneNumber phoneNumber)
  {
    SetContactPhoneNumber(contact, phoneNumber);
    return AddPhoneNumber(dbContext, phoneNumber);
  }

  static PhoneNumber AddPhoneNumber (AgendaContext dbContext, PhoneNumber phoneNumber) => AddEntity(dbContext, phoneNumber);

  static IEnumerable<PhoneNumber> AddPhoneNumbers (AgendaContext dbContext, IEnumerable<PhoneNumber> phoneNumbers)
  {
    foreach(var phoneNumber in phoneNumbers)
      AddPhoneNumber(dbContext, phoneNumber);
    return phoneNumbers;
  }
}