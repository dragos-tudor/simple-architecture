
namespace Simple.Domain.CodeGenerator;

partial class CodeGeneratorFuncs
{
  internal static bool HasSimpleBaseTypeChildNode (BaseListSyntax baseListSyntax, string nodeName) =>
    baseListSyntax.ChildNodes().Where(IsSimpleBaseTypeKind).Any(IsSyntaxNodeWithName(nodeName));

  static bool IsSimpleBaseTypeKind (SyntaxNode syntaxNode) => syntaxNode.IsKind(SyntaxKind.SimpleBaseType);

  static Func<SyntaxNode, bool> IsSyntaxNodeWithName (string nodeName) => (SyntaxNode syntaxNode) => syntaxNode.ToFullString() == nodeName;
}