
using System.Linq;

namespace Simple.Infrastructure.MailServer;

partial class MailServerTests
{
  [TestMethod]
  public async Task mail_message__send_mail_message__mail_message_received()
  {
    var options = new MailServerOptions();
    using var mailMessage = BuildMailMessage("a@test.com", "b@test.com", Guid.NewGuid().ToString(), "body");

    await SendMailMessageAsync(mailMessage, options);
    var actual = await ReceiveMailMessagesAsync("b@test.com", "b@test.com", options);

    var filterMail = (MimeMessage _mailMessage) => _mailMessage.Subject == mailMessage.Subject;
    AreEqual(actual.Where(filterMail).Select(GetMessageFrom), ["a@test.com"]);
  }
}