
global using static Simple.Shared.Testing.TestingFuncs;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Simple.Infrastructure.EmailServer;

[TestClass]
public partial class NotificationsTests
{
  static readonly EmailServerOptions EmailServerOptions = new ();

  [AssemblyInitialize]
  public static void InitializeEmailServer (TestContext _)
  {
    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(5));
    var cancellationToken = cancellationTokenSource.Token;

    RunSynchronously(() =>
      InitializeEmailServerAsync (EmailServerOptions, cancellationToken));
  }
}