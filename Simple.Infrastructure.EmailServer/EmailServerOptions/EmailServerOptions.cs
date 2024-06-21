
namespace Simple.Infrastructure.EmailServer;

public partial record EmailServerOptions
{
  public string ImageName { get; init; } = "greenmail/standalone:2.0.1";
  public string ContainerName { get; init; } = "simple-email";
  public string NetworkName { get; init; } = "simple-network";
  public int ImapPort { get; init; } = 3143;
  public int Pop3Port { get; init; } = 3110;
  public int SmtpPort { get; init; } = 3025;
}

partial record EmailServerOptions
{
  public void Deconstruct(
    out string imageName,
    out string containerName,
    out string networkName,
    out int imapPort,
    out int pop3Port,
    out int smtpPort)
  {
    imageName = ImageName;
    containerName = ContainerName;
    networkName = NetworkName;
    imapPort = ImapPort;
    pop3Port = Pop3Port;
    smtpPort = SmtpPort;
  }
}