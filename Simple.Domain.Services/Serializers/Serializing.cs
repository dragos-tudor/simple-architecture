
using System.Text.Json;

namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static string SerializeObject<T>(T value, JsonSerializerOptions? options = default) =>
    JsonSerializer.Serialize(value, options);
}