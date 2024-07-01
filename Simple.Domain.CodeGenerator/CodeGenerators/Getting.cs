
namespace Simple.Domain.CodeGenerator;

partial class CodeGeneratorFuncs
{
  internal static string GetMatchMessageTypeStatement (string className) => $"nameof({className}) => RestoreMessage(message, DeserializePayload<{className}>(message.MessageContent)!),";

  internal static IEnumerable<string> GetMatchMessageTypeStatements (IEnumerable<string> classNames) => classNames.Select(GetMatchMessageTypeStatement);

  internal static string GetRestoreMessageDeclaration (IEnumerable<string> matchMessageTypeStatements) => $@"
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