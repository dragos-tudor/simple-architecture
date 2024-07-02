
namespace Simple.Domain.CodeGenerator;

class RestoreMessageSyntaxReceiver : ISyntaxReceiver
{
  public ICollection<string> EventClassesNames { get; } = [];
  public ICollection<string> NotificationClassesNames { get; } = [];

  public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
  {
    if (syntaxNode is not RecordDeclarationSyntax recordDeclarationSyntax) return;
    if (recordDeclarationSyntax.BaseList is not BaseListSyntax baseListSyntax) return;

    if (IsImplementingIEventInterface(baseListSyntax)) EventClassesNames.Add(GetRecordDeclarationSyntaxName(recordDeclarationSyntax));
    if (IsImplementingINotificationInterface(baseListSyntax)) NotificationClassesNames.Add(GetRecordDeclarationSyntaxName(recordDeclarationSyntax));
  }
}
