
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsTests
{
  [TestMethod]
  public async Task mail_message__send_mail_message__mail_server_receive_mail()
  {
    var mailMessages = new List<MimeMessage>();
    var smtpServer = CreateSmtpServer("localhost", 9025, new SmtpServerStore(mailMessages));
    _ = smtpServer.StartAsync(CancellationToken.None); // WaitingForActivation status!

    using var smtpClient = CreateSmtpClient();
    using var mailMessage = BuildMailMessage("a@test.com", "b@test.com", "subject1", "body1");
    await SendMailMessage(smtpClient, mailMessage, "localhost", 9025);
    smtpServer.Shutdown();

    var actual = GetMailMessage(mailMessages, mailMessage.Subject);
    StringAssert.Contains(actual!.TextBody, "body1", StringComparison.InvariantCulture);
  }
}