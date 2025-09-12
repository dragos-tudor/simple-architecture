
namespace Simple.Infrastructure.MailServer;

partial class MailServerFuncs
{
  public static async Task<IEnumerable<MimeMessage>> ReceiveMailMessagesAsync(
    string userName,
    string userPassword,
    MailServerOptions options,
    CancellationToken cancellationToken = default)
  {
    using var client = CreateImapClient();
    return await ReceiveMailMessagesAsync(client, userName, userPassword, options, cancellationToken);
  }
}