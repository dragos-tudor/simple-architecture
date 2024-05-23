using static System.Enum;
using static System.Linq.Enumerable;
#pragma warning disable CA5394

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerTests
{
  static int GetRandomYear () => GetRandomInt(DateTime.MinValue.Year, DateTime.MaxValue.Year);

  static int GetRandomMonth () => GetRandomInt(1, 12);

  static int GetRandomDay () => GetRandomInt(1, 28);

  static char GetRandonPrintableChar (int _) => (char)GetRandomInt(33, 126);


  public static DateTime GetRandomDate () => new ( GetRandomYear(), GetRandomMonth(), GetRandomDay() );

  public static T GetRandomEnum<T> () where T: struct, Enum => GetValues<T>()[GetRandomInt(0, GetValues<T>().Length)];

  public static Guid GetRandomGuid () => Guid.NewGuid();

  public static int GetRandomInt (int incluiveMin = 0, int inclusiveMax = 10000) => Random.Shared.Next(incluiveMin, inclusiveMax + 1);

  public static string GetRandomString (int length) => new (Range(0, length).Select(GetRandonPrintableChar).ToArray());
}
