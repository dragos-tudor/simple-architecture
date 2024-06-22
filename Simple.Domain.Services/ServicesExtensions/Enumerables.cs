
namespace Simple.Domain.Services;

partial class ServicesFuncs
{
  static T[] ToArray<T> (IEnumerable<T> values) => values.ToArray();
}