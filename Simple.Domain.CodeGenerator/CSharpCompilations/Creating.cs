
namespace Simple.Domain.CodeGenerator;

partial class CodeGeneratorFuncs
{
  internal static CSharpCompilation CreateCompilation(string source) =>
    CSharpCompilation.Create("compilation",
      [CSharpSyntaxTree.ParseText(source)],
      [MetadataReference.CreateFromFile(typeof(Binder).GetTypeInfo().Assembly.Location)],
      new CSharpCompilationOptions(OutputKind.ConsoleApplication));
}