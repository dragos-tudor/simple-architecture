
namespace Simple.Web.Api;

partial class ApiFuncs
{
  static async Task FinalizeSqlMessage (Message message, AgendaContextFactory agendaContextFactory, CancellationToken cancellationToken = default)
  {
    using var agendaContext = await agendaContextFactory.CreateDbContextAsync(cancellationToken);
    await UpdateMessageIsActive(agendaContext, message, false, cancellationToken);
  }

  internal static Task ConsumeSqlMessages (Channel<Message> messageQueue, Subscriber<Message, Failure>[] subscribers, AgendaContextFactory agendaContextFactory) =>
    ConsumeMessages(
      messageQueue,
      (message, cancellationToken) => HandleMessage(message, GetMessageType(message)!, subscribers, cancellationToken),
      (message, cancellationToken) => FinalizeSqlMessage(message, agendaContextFactory, cancellationToken),
      Logger
    );
}