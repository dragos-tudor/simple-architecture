
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  static void DeletePhoneNumber (Contact contact, PhoneNumber phoneNumber)
  {
    if(GetPhoneNumber(contact.PhoneNumbers, phoneNumber) is PhoneNumber contactPhoneNumber)
      contact.PhoneNumbers.Remove(contactPhoneNumber);
  }

  public static Contact DeletePhoneNumber (AgendaContext dbContext, Contact contact, PhoneNumber phoneNumber) =>
    SqlFuncs.UpdateEntity(dbContext, contact,
      (contact) => DeletePhoneNumber(contact, phoneNumber));
}