
using System.Text.Json;

namespace Simple.Shared.Models;

partial class ModelsFuncs
{
  public static T? DeserializeJson<T>(string json, JsonSerializerOptions? options = default) =>
    JsonSerializer.Deserialize<T>(json, options);
}