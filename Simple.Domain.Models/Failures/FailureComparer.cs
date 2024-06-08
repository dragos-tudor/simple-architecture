
using System.Collections;

namespace Simple.Domain.Models;

public sealed class FailureComparer : IComparer
{
  public int Compare(object? x, object? y) =>
    (x, y) switch {
      (not null, not null) => x.ToString() == y.ToString() ? 0: 1,
      (null, not null) => -1,
      (not null, null) => 1,
      (null, null) => 0
    };
}