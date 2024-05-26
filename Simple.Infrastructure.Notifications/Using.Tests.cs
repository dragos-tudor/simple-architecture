
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Simple.Infrastructure.Notifications;

[TestClass]
public partial class NotificationsTests
{
  static SmtpServer.SmtpServer SmtpServer = default!;
  static SmtpServerOptions SmtpServerOptions = default!;

  [AssemblyInitialize]
  public static void StartSmtpServer(TestContext _)
  {
    SmtpServerOptions = CreateSmtpServerOptions("localhost", 9025);
    SmtpServer = CreateSmtpServer<SmtpServerStore>(SmtpServerOptions.ServerName, SmtpServerOptions.SewrverPort);
    SmtpServer.StartAsync(CancellationToken.None); // WaitingForActivation status!
  }

  [AssemblyCleanup]
  public static void ShutdownSmtpServer() => SmtpServer.Shutdown();
}