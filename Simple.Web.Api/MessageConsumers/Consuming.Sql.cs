
namespace Simple.Web.Api;

partial class ApiFuncs
{
  internal static Task ConsumeSqlMessages (Channel<Message> messageQueue, Subscriber<Message>[] subscribers, AgendaContextFactory agendaContextFactory) =>
    ConsumeMessages(messageQueue, subscribers, async (message, cancellationToken) => {
      using var agendaContext = await agendaContextFactory.CreateDbContextAsync(cancellationToken);
      await UpdateMessageIsActive(agendaContext, message, false, cancellationToken);
    });
}