
namespace Simple.Domain.CodeGenerator;

class SyntaxReceiver : ISyntaxReceiver
{
  public ICollection<string> EventClassesNames { get; } = [];
  public ICollection<string> NotificationClassesNames { get; } = [];

  public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
  {
    if (syntaxNode is not RecordDeclarationSyntax recordDeclaration) return;
    if (recordDeclaration.BaseList is not BaseListSyntax baseList) return;

    if (HasSimpleBaseTypeChildNode(baseList, "IEvent"))
      EventClassesNames.Add(recordDeclaration.Identifier.ValueText);

    if (HasSimpleBaseTypeChildNode(baseList, "INotification"))
      NotificationClassesNames.Add(recordDeclaration.Identifier.ValueText);
  }
}
