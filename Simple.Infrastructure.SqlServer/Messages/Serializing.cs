
using System.Text.Json;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static string SerializeMessagePayload<T>(T payload) =>
    JsonSerializer.Serialize(payload);
}