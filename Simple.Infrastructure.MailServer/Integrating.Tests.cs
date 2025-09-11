
using System.Linq;

namespace Simple.Infrastructure.MailServer;

partial class EmailServerTests
{
  [TestMethod]
  public async Task email__send_email__email_received()
  {
    using var smtpClient = CreateSmtpClient();
    using var imapClient = CreateImapClient();
    var mail = new TestMail("x@test.com", "y@test.com", Guid.NewGuid().ToString());
    using var message = BuildMailMessage(mail.From, mail.To, mail.Subject, "");

    await SendMailMessageAsync(smtpClient, message, EmailServerName, SmtpServerPort);
    var actual = await ReceiveMailMessagesAsync(imapClient, mail.To, mail.To, EmailServerName, ImapServerPort);

    var mapMessage = (MimeMessage message) => new TestMail(GetMessageFrom(message), GetMessageTo(message), GetMessageSubject(message));
    var filterMail = (TestMail receivedMail) => receivedMail.Subject == mail.Subject;
    CollectionAssert.AreEqual(actual.Select(mapMessage).Where(filterMail).ToList(), new List<TestMail>() { mail });
  }

  sealed record TestMail(string From, string To, string Subject);
}