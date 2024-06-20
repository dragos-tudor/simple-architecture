
using Docker.DotNet.Models;

namespace Simple.Infrastructure.Notifications;

partial class NotificationsFuncs
{
  static Action<CreateContainerParameters> SetEmailCreateContainerParameters (string containerName, int imapPort, int pop3Port, int smtpPort, string networkName) => (CreateContainerParameters @params) =>
  {
    @params.Hostname = containerName;
    @params.HostConfig = new HostConfig() { NetworkMode = networkName };
    @params.ExposedPorts = new Dictionary<string, EmptyStruct> () {
      {$"${imapPort}:3143", new EmptyStruct()},
      {$"${pop3Port}:3110", new EmptyStruct()},
      {$"${smtpPort}:3025", new EmptyStruct()}
    };
    // @params.Env = ["GREENMAIL_OPTS"];
  };
}