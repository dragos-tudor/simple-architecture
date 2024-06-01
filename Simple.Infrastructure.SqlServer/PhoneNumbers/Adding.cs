
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  static void AddPhoneNumber (Contact contact, PhoneNumber phoneNumber) =>
    contact.PhoneNumbers!.Add(phoneNumber);

  static PhoneNumber AddPhoneNumber (AgendaContext dbContext, PhoneNumber phoneNumber) =>
    AddEntity(dbContext, phoneNumber);

  static List<PhoneNumber> AddPhoneNumbers (AgendaContext dbContext, IEnumerable<PhoneNumber> phoneNumbers) =>
    phoneNumbers.Select(phoneNumber => AddPhoneNumber(dbContext, phoneNumber)).ToList();

  public static Contact AddPhoneNumber (AgendaContext dbContext, Contact contact, PhoneNumber phoneNumber) =>
    UpdateEntity(dbContext, contact, (contact) => AddPhoneNumber(contact, phoneNumber));
}