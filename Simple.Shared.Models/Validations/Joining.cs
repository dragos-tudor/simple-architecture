
namespace Simple.Shared.Models;

partial class ModelsFuncs
{
  public static string JoinValidationErrors (IEnumerable<string> errors) => string.Join(Environment.NewLine, errors);
}