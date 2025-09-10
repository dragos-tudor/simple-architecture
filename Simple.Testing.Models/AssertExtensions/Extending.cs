
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Simple.Testing.Models;

partial class ModelsFuncs
{
  public static void AreEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual) =>
    CollectionAssert.AreEqual(expected.ToList(), actual.ToList());

  public static void Contains<T>(IEnumerable<T> expected, IEnumerable<T> actual) =>
    CollectionAssert.Contains(expected.ToList(), actual.ToList());

  public static void DoesNotContain<T>(IEnumerable<T> expected, IEnumerable<T> actual) =>
    CollectionAssert.DoesNotContain(expected.ToList(), actual.ToList());
}