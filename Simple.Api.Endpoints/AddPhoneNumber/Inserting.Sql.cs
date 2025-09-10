
namespace Simple.Api.Endpoints;

partial class EndpointsFuncs
{
  public static Task InsertPhoneNumberSqlAsync(AgendaContext dbContext, Contact contact, PhoneNumber phoneNumber, CancellationToken cancellationToken = default)
  {
    SetContactPhoneNumber(contact, phoneNumber);
    AddPhoneNumber(dbContext, phoneNumber);
    return SaveChangesAsync(dbContext, cancellationToken);
  }
}