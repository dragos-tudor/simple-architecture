
using System.Text.Json;

namespace Simple.Domain.Models;

partial class ModelsFuncs
{
  public static object? DeserializeMessagePayload (Message message) => DeserializePayload(message.MessageContent, GetMessagePayloadType(GetMessagePayloadAssembly(), message));

  public static T? DeserializePayload<T> (string json, JsonSerializerOptions? options = default) => JsonSerializer.Deserialize<T>(json, options);

  public static object? DeserializePayload (string json, Type type, JsonSerializerOptions? options = default) => JsonSerializer.Deserialize(json, type, options);
}