
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static Task InsertContactAndMessage (AgendaContext dbContext, Contact contact, Message message, CancellationToken cancellationToken = default)
  {
    AddContact(dbContext, contact);
    AddPhoneNumbers(dbContext, contact.PhoneNumbers ?? []);
    AddMessage(dbContext, message);
    return SaveChanges(dbContext, cancellationToken);
  }

  public static Task InsertContactPhoneNumber (AgendaContext dbContext, Contact contact, PhoneNumber phoneNumber, CancellationToken cancellationToken = default)
  {
    UpdateEntity(dbContext, contact, (contact) => AddContactPhoneNumber(dbContext, contact, phoneNumber));
    return SaveChanges(dbContext, cancellationToken);
  }
}