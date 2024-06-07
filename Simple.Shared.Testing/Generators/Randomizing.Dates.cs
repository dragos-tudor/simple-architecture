
namespace Simple.Shared.Testing;

partial class TestingFuncs
{
  static int GetRandomDay () => GetRandomInt(1, 28);

  static int GetRandomMonth () => GetRandomInt(1, 12);

  static int GetRandomYear () => GetRandomInt(DateTime.MinValue.Year, DateTime.MaxValue.Year);

  public static DateTime GetRandomDate () => new ( GetRandomYear(), GetRandomMonth(), GetRandomDay() );
}
