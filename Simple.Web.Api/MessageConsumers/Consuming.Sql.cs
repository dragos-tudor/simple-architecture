
namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static Task ConsumeSqlMessages (Channel<Message> messageQueue, Subscriber<Message, Failure>[] subscribers, AgendaContextFactory agendaContextFactory, ILogger logger, CancellationToken cancellationToken = default) =>
    ConsumeMessages(
      messageQueue,
      (message, cancellationToken) => HandleMessage(message, GetMessageType(message)!, subscribers, cancellationToken),
      (message, cancellationToken) => FinalizeSqlMessage(message, agendaContextFactory, cancellationToken),
      logger,
      cancellationToken
    );
}