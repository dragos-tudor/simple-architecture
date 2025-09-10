
namespace Simple.Domain.CodeGenerator;

partial class CodeGeneratorFuncs
{
  internal static bool IsImplementingIEventInterface(BaseListSyntax baseListSyntax) => HasSimpleBaseTypeChildNode(baseListSyntax, "IEvent");

  internal static bool IsImplementingINotificationInterface(BaseListSyntax baseListSyntax) => HasSimpleBaseTypeChildNode(baseListSyntax, "INotification");
}