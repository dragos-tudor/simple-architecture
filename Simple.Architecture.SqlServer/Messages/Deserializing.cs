
using System.Text.Json;

namespace Simple.Architecture.SqlServer;

partial class SqlServerFuncs
{
  public static T? DeserializeMessagePayload<T>(MessageDb message) =>
    JsonSerializer.Deserialize<T>(message.MessageContent);
}