
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Contact AddContact (AgendaContext dbContext, Contact contact)
  {
    SqlFuncs.AddEntity(dbContext, contact);
    AddPhoneNumbers(dbContext, contact.PhoneNumbers);
    return contact;
  }

  public static Task AddAndStoreContactAndMessage (AgendaContext dbContext, Contact contact, Message message, CancellationToken cancellationToken = default)
  {
    AddContact(dbContext, contact);
    AddMessage(dbContext, message);
    return SaveChanges(dbContext, cancellationToken);
  }
}