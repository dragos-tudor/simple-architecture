
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Contact UpdateContact(AgendaContext dbContext, Contact contact) => UpdateEntity(dbContext, contact, (contact) => { });

  public static Contact UpdateContact(AgendaContext dbContext, Contact contact, Action<Contact> updateContact) => UpdateEntity(dbContext, contact, updateContact);
}