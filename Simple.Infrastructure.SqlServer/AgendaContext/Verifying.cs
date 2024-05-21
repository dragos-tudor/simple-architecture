
namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static bool IsInMemoryContext () => Environment.GetEnvironmentVariable("IN_MEMORY")! == "true";
}