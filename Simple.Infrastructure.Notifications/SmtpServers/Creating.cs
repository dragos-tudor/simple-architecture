
using Microsoft.Extensions.DependencyInjection;
using SmtpServer;
using SmtpServer.Storage;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  public static SmtpServer.SmtpServer CreateSmtpServer<TStore> (string serverName, int serverPort, TStore messageStore) where TStore: class, IMessageStore
  {
    var smtpServerOptions = new SmtpServerOptionsBuilder().ServerName(serverName).Port(serverPort).Build();
    var serviceProvider = new ServiceCollection().AddSingleton<IMessageStore>(messageStore).BuildServiceProvider();

    return new SmtpServer.SmtpServer(smtpServerOptions, serviceProvider);
  }
}