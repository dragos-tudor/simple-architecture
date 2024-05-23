
using System.Text.Json;

namespace Simple.Shared.Extensions;

partial class ExtensionsFuncs
{
  public static string SerializeObject<T>(T obj, JsonSerializerOptions? options = default) where T: class =>
    JsonSerializer.Serialize(obj, options);
}