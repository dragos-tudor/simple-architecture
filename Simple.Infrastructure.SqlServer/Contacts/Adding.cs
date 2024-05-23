
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Contact AddContact (AgendaContext dbContext, Contact contact)
  {
    SqlFuncs.AddEntity(dbContext, contact);
    AddPhoneNumbers(dbContext, contact.PhoneNumbers);
    return contact;
  }
}