
namespace Simple.Shared.Extensions;

partial class ExtensionsFuncs
{
  public static string JoinErrors<T> (IEnumerable<T> errors) where T: Exception => string.Join(Environment.NewLine, errors.Select(error => error.Message));
}