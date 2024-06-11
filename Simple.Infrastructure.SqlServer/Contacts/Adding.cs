
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Contact AddContact (AgendaContext dbContext, Contact contact) => AddEntity(dbContext, contact);

  public static PhoneNumber AddContactPhoneNumber (AgendaContext dbContext, Contact contact, PhoneNumber phoneNumber)
  {
    SetContactPhoneNumber(contact, phoneNumber);
    return AddPhoneNumber(dbContext, phoneNumber);
  }
}