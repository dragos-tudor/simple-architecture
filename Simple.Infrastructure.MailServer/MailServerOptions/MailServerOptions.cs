
namespace Simple.Infrastructure.MailServer;

public record MailServerOptions
{
  public string MailServerName { get; init; } = "127.0.0.1";
  public int SmtpPort { get; init; } = 3025;
  public int Pop3Port { get; init; } = 3110;
  public int ImapPort { get; init; } = 3143;
}