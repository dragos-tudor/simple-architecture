
using Docker.DotNet;

namespace Simple.Infrastructure.MongoDb;

partial class MongoDbFuncs
{
  public static async Task WaitForMongoReplicaSet (IExecOperations exec, string containerId, int serverPort, string replicaSet, string[] containerNames, CancellationToken cancellationToken = default)
  {
    var retryAfter = TimeSpan.FromMilliseconds(1000);
    var verifyStatusCommand = GetVerifyReplicaSetStatusCommand(serverPort, replicaSet, containerNames);

    await WaitUntilAsync(
      async () => await ExecContainerCommandAsync(exec, containerId, verifyStatusCommand, cancellationToken) == 0,
      retryAfter, cancellationToken);
  }
}