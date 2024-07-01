
namespace Simple.Domain.CodeGenerator;

partial class CodeGeneratorFuncs
{
  internal static string GenerateRestoreMessageFunction (IEnumerable<string> matchMessageTypeStatements) => $@"
namespace Simple.Domain.Models;

partial class ModelsFuncs
{{
  public static partial Message RestoreMessage (Message message) =>
    message.MessageType switch {{
        {string.Join("\n", matchMessageTypeStatements)}
      _ => throw new NotImplementedException(""Restoring message with type "" + message.MessageType + "" not implemented."")
  }};
}}";

  internal static string GenerateMatchMessageTypeBranch (string className) => $"nameof({className}) => RestoreMessage(message, DeserializePayload<{className}>(message.MessageContent)!),";

  internal static IEnumerable<string> GenerateMatchMessageTypeBranches (IEnumerable<string> classNames) => classNames.Select(GenerateMatchMessageTypeBranch);
}