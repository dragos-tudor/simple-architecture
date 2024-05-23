
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  static string GetCreateSqlDatabaseScript (string databaseName) => $@"
    USE MASTER;
    IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = '{databaseName}')
    BEGIN
      CREATE DATABASE [{databaseName}];
    END;
  ";
}