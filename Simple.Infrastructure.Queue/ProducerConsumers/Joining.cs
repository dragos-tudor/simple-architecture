
using System.Linq;

namespace Simple.Infrastructure.Queue;

partial class QueueFuncs
{
  static string JoinFailures<TFailure> (IEnumerable<TFailure> failures, char separator = '\n') => string.Join(separator, failures.Where(ExistFailure).Select(failure => failure!.ToString()));
}