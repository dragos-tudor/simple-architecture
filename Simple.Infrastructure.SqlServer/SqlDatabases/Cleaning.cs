
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  internal static int CleanAgendaDatabase (AgendaContext dbContext) =>
    dbContext.Database.ExecuteSqlRaw(@"
      DELETE FROM [PhoneNumbers];
      DELETE FROM [Contacts];
      DELETE FROM [Messages];
    ");
}