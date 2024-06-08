
using System.Linq;

namespace Simple.Infrastructure.Queue;

partial class QueueFuncs
{
  static bool ExistFailure<TFailure> (TFailure? failure) => failure is not null;

  static bool ExistsFailures<TFailure> (IEnumerable<TFailure> failures) => failures.Any();
}