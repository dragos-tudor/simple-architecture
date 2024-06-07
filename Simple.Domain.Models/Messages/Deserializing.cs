
using System.Text.Json;

namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static T? DeserializePayload<T>(string json, JsonSerializerOptions? options = default) =>
    JsonSerializer.Deserialize<T>(json, options);
}