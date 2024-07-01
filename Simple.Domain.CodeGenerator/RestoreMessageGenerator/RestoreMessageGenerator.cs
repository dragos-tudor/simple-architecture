
using Microsoft.CodeAnalysis.Text;

namespace Simple.Domain.CodeGenerator;

[Generator]
public partial class RestoreMessageGenerator : ISourceGenerator
{
  public void Initialize(GeneratorInitializationContext context)
  {
    context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
  }

  public void Execute(GeneratorExecutionContext context)
  {
    var syntaxReceiver = (SyntaxReceiver)context.SyntaxReceiver!;
    var classNames = syntaxReceiver.EventClassesNames.Concat(syntaxReceiver.NotificationClassesNames);

    var matchMessageTypeBranches = GenerateMatchMessageTypeBranches(classNames);
    var restoreMessageFunction = GenerateRestoreMessageFunction(matchMessageTypeBranches);

    context.AddSource("Restoring.g.cs", SourceText.From(restoreMessageFunction, Encoding.UTF8));
  }
}