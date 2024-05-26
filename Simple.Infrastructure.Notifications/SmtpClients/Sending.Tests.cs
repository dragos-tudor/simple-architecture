
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsTests
{
  [TestMethod]
  public async Task mail_message__send_mail_message__mail_server_receive_mail()
  {
    using var mailMessage = BuildMailMessage("a@test.com", "b@test.com", "subject1", "body1");
    using var smtpClient = CreateSmtpClient();
    await SendMailMessage(smtpClient, mailMessage, SmtpServerOptions);

    Assert.IsNotNull(GetMailMessage(mailMessage.Subject));
    StringAssert.Contains(GetMailMessage(mailMessage.Subject)!.TextBody, "body1", System.StringComparison.InvariantCulture);
  }
}