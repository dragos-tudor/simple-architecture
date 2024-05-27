using static System.Enum;
using static System.Linq.Enumerable;
#pragma warning disable CA5394

namespace Simple.Domain.Models;

partial class ModelsTests
{
  static int GetRandomDay () => GetRandomInt(1, 28);

  static int GetRandomMonth () => GetRandomInt(1, 12);

  static char GetRandomPrintableChar (int _) => (char)GetRandomInt(33, 126);

  static char GetRandomLetter (int _) => (char)GetRandomInt(97, 122);

  static int GetRandomYear () => GetRandomInt(DateTime.MinValue.Year, DateTime.MaxValue.Year);


  public static DateTime GetRandomDate () => new ( GetRandomYear(), GetRandomMonth(), GetRandomDay() );

  public static T GetRandomEnum<T> () where T: struct, Enum => GetValues<T>()[GetRandomInt(0, GetValues<T>().Length)];

  public static string GetRandomEmail (int length) => GetRandomLetters(length - 8) + "@" + GetRandomLetters(4) +  "." + GetRandomLetters(2);

  public static Guid GetRandomGuid () => Guid.NewGuid();

  public static short GetRandomShort (short inclusiveMin = 0, short inclusiveMax = 10000) => (short)Random.Shared.Next(inclusiveMin, inclusiveMax + 1);

  public static int GetRandomInt (int inclusiveMin = 0, int inclusiveMax = 10000) => Random.Shared.Next(inclusiveMin, inclusiveMax + 1);

  public static long GetRandomLong (long inclusiveMin = 0, long inclusiveMax = 10000) => Random.Shared.NextInt64(inclusiveMin, inclusiveMax + 1);

  public static string GetRandomString (int length) => new (Range(0, length).Select(GetRandomPrintableChar).ToArray());

  public static string GetRandomLetters (int length) => new (Range(0, length).Select(GetRandomLetter).ToArray());
}
