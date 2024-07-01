
namespace Simple.Domain.CodeGenerator;

partial class CodeGeneratorFuncs
{
  internal static string GetRecordDeclarationSyntaxName (RecordDeclarationSyntax recordDeclarationSyntax) => recordDeclarationSyntax.Identifier.ValueText;
}