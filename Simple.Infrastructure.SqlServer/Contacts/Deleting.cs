

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static void DeleteContactPhoneNumber (AgendaContext dbContext, Contact contact, PhoneNumber phoneNumber)
  {
    contact.PhoneNumbers = (contact.PhoneNumbers ?? []).Where(e => EqualPhoneNumbers(e, phoneNumber)).ToList();
    DeletePhoneNumber(dbContext, phoneNumber);
  }
}