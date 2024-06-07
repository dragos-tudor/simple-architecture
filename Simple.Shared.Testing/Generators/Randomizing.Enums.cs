using static System.Enum;

namespace Simple.Shared.Testing;

partial class TestingFuncs
{
  public static T GetRandomEnum<T> () where T: struct, Enum => GetValues<T>()[GetRandomInt(0, GetValues<T>().Length)];
}
