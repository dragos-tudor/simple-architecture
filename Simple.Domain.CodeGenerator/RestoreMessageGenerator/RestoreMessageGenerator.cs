
using Microsoft.CodeAnalysis.Text;

namespace Simple.Domain.CodeGenerator;

[Generator]
public partial class RestoreMessageGenerator : ISourceGenerator
{
  public void Initialize(GeneratorInitializationContext context)
  {
    context.RegisterForSyntaxNotifications(() => new RestoreMessageSyntaxReceiver());
  }

  public void Execute(GeneratorExecutionContext context)
  {
    var syntaxReceiver = (RestoreMessageSyntaxReceiver)context.SyntaxReceiver!;
    var classNames = syntaxReceiver.EventClassesNames;

    var matchMessageTypeBranches = GenerateMatchMessageTypeBranches(classNames);
    var restoreMessageFunction = GenerateRestoreMessageFunction(matchMessageTypeBranches);

    context.AddSource("Restoring.g.cs", SourceText.From(restoreMessageFunction, Encoding.UTF8));
  }
}