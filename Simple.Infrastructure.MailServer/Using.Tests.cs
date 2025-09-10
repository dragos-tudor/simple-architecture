
global using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Simple.Infrastructure.EmailServer;

[TestClass]
public partial class EmailServerTests
{
  static readonly string EmailServerName = "localhost";
  static readonly int SmtpServerPort = 3025;
  static readonly int ImapServerPort = 3143;
}