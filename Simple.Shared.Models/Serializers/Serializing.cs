
using System.Text.Json;

namespace Simple.Shared.Models;

partial class ModelsFuncs
{
  public static string SerializeObject<T>(T value, JsonSerializerOptions? options = default) =>
    JsonSerializer.Serialize(value, options);
}