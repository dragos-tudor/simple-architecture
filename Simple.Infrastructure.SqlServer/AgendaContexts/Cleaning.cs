
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static int CleanAgendaDatabase (AgendaContext dbContext) =>
    dbContext.Database.ExecuteSqlRaw(@"
      IF OBJECT_ID(N'[Contacts]') IS NOT NULL
      BEGIN
        DELETE FROM [PhoneNumbers];
        DELETE FROM [Contacts];
        DELETE FROM [Messages];
      END
    ");
}