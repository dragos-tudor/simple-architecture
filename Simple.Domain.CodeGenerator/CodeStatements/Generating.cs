
namespace Simple.Domain.CodeGenerator;

partial class CodeGeneratorFuncs
{
  internal static string GenerateMatchMessageTypeStatement (string className) => $"nameof({className}) => RestoreMessage(message, DeserializePayload<{className}>(message.MessageContent)!),";

  internal static IEnumerable<string> GenerateMatchMessageTypeStatements (IEnumerable<string> classNames) => classNames.Select(GenerateMatchMessageTypeStatement);

  internal static string GenerateRestoreMessageDeclaration (IEnumerable<string> matchMessageTypeStatements) => $@"
namespace Simple.Domain.Models;

partial class ModelsFuncs
{{
  public static partial Message RestoreMessage (Message message) =>
    message.MessageType switch {{
        {string.Join("\n", matchMessageTypeStatements)}
      _ => throw new NotImplementedException(""Restoring message with type "" + message.MessageType + "" not implemented."")
  }};
}}";
}