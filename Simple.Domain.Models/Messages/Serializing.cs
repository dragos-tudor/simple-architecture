
using System.Text.Json;

namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static string SerializePayload<T>(T payload, JsonSerializerOptions? options = default) => JsonSerializer.Serialize(payload, options);
}