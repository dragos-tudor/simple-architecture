
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

    var matchMessageTypeStatements = GetMatchMessageTypeStatements(classNames);
    var restoreMessageDeclaration = GetRestoreMessageDeclaration(matchMessageTypeStatements);

    context.AddSource("Restoring.g.cs", SourceText.From(restoreMessageDeclaration, Encoding.UTF8));
  }
}