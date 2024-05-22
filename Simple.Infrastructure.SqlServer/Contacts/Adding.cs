using SqlFuncs = Storing.SqlServer.SqlServerFuncs;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  static void AddContact (AgendaContext dbContext, Contact contact) => SqlFuncs.AddEntity(dbContext, contact);

  public static void AddContact (AgendaContext dbContext, Contact contact, params PhoneNumber[] phoneNumbers)
  {
    AddContact(dbContext, contact);
    AddPhoneNumbers(dbContext, phoneNumbers);
    SetContactPhoneNumbers(contact, phoneNumbers);
  }

  public static void AddContactPhoneNumber (AgendaContext dbContext, Contact contact, PhoneNumber phoneNumber)
  {
    dbContext.Attach(contact); // TODO: add attach entity to storing sql library;
    AddPhoneNumber(dbContext, phoneNumber);
    SetContactPhoneNumber(contact, phoneNumber);
  }
}