
using System.Text.Json;

namespace Simple.Infrastructure.SqlServer;

partial class SqlServerFuncs
{
  public static T? DeserializeMessagePayload<T>(Message message) =>
    JsonSerializer.Deserialize<T>(message.MessageContent);
}