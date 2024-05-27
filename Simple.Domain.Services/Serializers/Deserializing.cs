
using System.Text.Json;

namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static T? DeserializeJson<T>(string json, JsonSerializerOptions? options = default) =>
    JsonSerializer.Deserialize<T>(json, options);
}