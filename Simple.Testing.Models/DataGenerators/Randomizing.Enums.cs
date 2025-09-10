using static System.Enum;

namespace Simple.Testing.Models;

partial class ModelsFuncs
{
  public static T GetRandomEnum<T>() where T : struct, Enum => GetValues<T>()[GetRandomInt(0, GetValues<T>().Length)];
}
