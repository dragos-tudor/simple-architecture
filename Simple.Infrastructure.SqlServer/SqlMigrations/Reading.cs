
using System.IO;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  static IEnumerable<string> ReadSqlMigrations (string directory) =>
    Directory.EnumerateFiles(directory).Select(File.ReadAllText);
}