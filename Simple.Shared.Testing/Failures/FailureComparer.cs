
using System.Collections;

namespace Simple.Shared.Testing;

public sealed class FailureComparer : IComparer
{
  public int Compare(object? x, object? y) =>
    (x, y) switch {
      (not null, not null) =>
        (x.GetType() == y.GetType() &&
        (x as Failure)!.Message == (y as Failure)!.Message)?
          0: 2,
      (null, not null) => -1,
      (not null, null) => 1,
      (null, null) => 0
    };
}