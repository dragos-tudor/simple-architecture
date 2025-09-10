
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  public static string? JoinErrors(IEnumerable<string?> errors) => string.Join(Environment.NewLine, errors.Where(ExistsError));
}