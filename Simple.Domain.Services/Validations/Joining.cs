
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static string JoinValidationErrors (IEnumerable<string> errors) => string.Join(Environment.NewLine, errors);
}