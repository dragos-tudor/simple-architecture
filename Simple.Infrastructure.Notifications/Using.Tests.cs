
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Simple.Infrastructure.Notifications;

[TestClass]
public partial class NotificationsTests
{
  static SmtpServer.SmtpServer SmtpServer = default!;
  static SmtpClientOptions SmtpClientOptions = default!;

  [AssemblyInitialize]
  public static void StartSmtpServer(TestContext _)
  {
    SmtpClientOptions = CreateSmtpClientOptions("localhost", 9025);
    SmtpServer = CreateSmtpServer<SmtpServerStore>(SmtpClientOptions.ServerName, SmtpClientOptions.SewrverPort);
    SmtpServer.StartAsync(CancellationToken.None); // WaitingForActivation state
  }

  [AssemblyCleanup]
  public static void ShutdownSmtpServer() => SmtpServer.Shutdown();
}