
namespace Simple.Infrastructure.JobScheduler;

partial class JobSchedulerFuncs
{
  static bool ExistMessages<TMessage>(IEnumerable<TMessage> messages) where TMessage : Message => messages.Any();
}