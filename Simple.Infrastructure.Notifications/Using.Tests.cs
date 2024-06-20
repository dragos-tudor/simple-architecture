
global using static Simple.Shared.Testing.TestingFuncs;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Simple.Infrastructure.Notifications;

[TestClass]
public partial class NotificationsTests
{
  static EmailServerOptions EmailServerOptions = default!;

  [AssemblyInitialize]
  public static void InitializeEmailServer (TestContext _)
  {
    using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(5));
    var cancellationToken = cancellationTokenSource.Token;
    var emailServerOptions = new EmailServerOptions();

    RunSynchronously(() =>
      InitializeEmailServerAsync (emailServerOptions, cancellationToken));
    EmailServerOptions = emailServerOptions;
  }
}