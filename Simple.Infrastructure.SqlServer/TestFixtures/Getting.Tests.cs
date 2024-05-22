#pragma warning disable CA5394

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
  public static DateTime GetRandomDate () =>
    new (
      GetRandomInt(DateTime.MinValue.Year, DateTime.MaxValue.Year),
      GetRandomInt(1, 12),
      GetRandomInt(1, 28)
    );

  public static Guid GetRandomGuid () => Guid.NewGuid();

  public static int GetRandomInt (int incluiveMin = 0, int inclusiveMax = 10000) =>
    Random.Shared.Next(incluiveMin, inclusiveMax + 1);

  public static string GetRandomString (int length) =>
    string.Join(string.Empty,
      Enumerable
        .Range(0, length / 36)
        .Select(_ => GetRandomGuid().ToString())
        .Append(GetRandomGuid().ToString().Substring(0, length % 36))
    );

}
