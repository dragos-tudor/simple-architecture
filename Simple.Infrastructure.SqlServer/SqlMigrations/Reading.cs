
using System.IO;
using System.Reflection;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  static string ReadSqlMigration (Stream stream)
  {
    using var fileStream = stream;
    using var streamReader = new StreamReader(fileStream);
    return streamReader.ReadToEnd();
  }

  static IEnumerable<string> ReadSqlMigrations (string directory) =>
    Directory.EnumerateFiles(directory).Select(File.ReadAllText);

  static IEnumerable<string> ReadSqlMigrations (Assembly assembly) =>
    Assembly.GetExecutingAssembly()
      .GetManifestResourceNames()
      .Where(IsSqlMigrationName)
      .Select(fileName => GetSqlMigrationStream(assembly, fileName))
      .Select(ReadSqlMigration);
}