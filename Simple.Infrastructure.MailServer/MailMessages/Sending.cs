
namespace Simple.Infrastructure.MailServer;

partial class MailServerFuncs
{
  public static async Task SendMailMessageAsync(
    MimeMessage mailMessage,
    MailServerOptions options,
    CancellationToken cancellationToken = default)
  {
    using var client = CreateSmtpClient();
    await SendMailMessageAsync(client, mailMessage, options, cancellationToken);
  }
}