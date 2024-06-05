
using System.IO;
using System.Reflection;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  static Stream GetSqlMigrationStream (Assembly assembly, string fileName) => assembly.GetManifestResourceStream(fileName)!;
}