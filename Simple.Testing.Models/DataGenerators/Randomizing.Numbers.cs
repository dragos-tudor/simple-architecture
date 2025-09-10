
#pragma warning disable CA5394

namespace Simple.Testing.Models;

partial class ModelsFuncs
{
  public static Guid GetRandomGuid() => Guid.NewGuid();

  public static int GetRandomInt(int inclusiveMin = 0, int inclusiveMax = 10000) => Random.Shared.Next(inclusiveMin, inclusiveMax + 1);

  public static long GetRandomLong(long inclusiveMin = 0, long inclusiveMax = 10000) => Random.Shared.NextInt64(inclusiveMin, inclusiveMax + 1);

  public static short GetRandomShort(short inclusiveMin = 0, short inclusiveMax = 10000) => (short)Random.Shared.Next(inclusiveMin, inclusiveMax + 1);
}
