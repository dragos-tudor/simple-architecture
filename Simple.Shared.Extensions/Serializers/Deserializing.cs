
using System.Text.Json;

namespace Simple.Shared.Extensions;

partial class ExtensionsFuncs
{
  public static T? DeserializeJson<T>(string json, JsonSerializerOptions? options = default) =>
    JsonSerializer.Deserialize<T>(json, options);
}