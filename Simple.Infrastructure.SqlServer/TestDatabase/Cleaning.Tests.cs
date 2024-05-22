
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
  static int CleanAgendaDatabase()
  {
    using var agendaContext = CreateAgendaContext();
    return agendaContext.Database.ExecuteSqlRaw(@"
      DELETE FROM [PhoneNumbers];
      DELETE FROM [Contacts];
      DELETE FROM [Messages];
    ");
  }
}