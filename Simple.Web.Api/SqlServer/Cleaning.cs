
using Microsoft.EntityFrameworkCore;

namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static int CleanAgendaDatabase (AgendaContext dbContext) =>
    dbContext.Database.ExecuteSqlRaw(@"
      DELETE FROM [PhoneNumbers];
      DELETE FROM [Contacts];
      DELETE FROM [Messages];
    ");
}