
namespace Simple.Infrastructure.JobScheduler;

partial class JobSchedulerFuncs
{
  static DateTime GetMinResumeDate (DateTime date, ResumeMessagesOptions resumeMessagesOptions) => date - resumeMessagesOptions.MinTime;

  static DateTime GetMaxResumeDate (DateTime date, ResumeMessagesOptions resumeMessagesOptions) => date - resumeMessagesOptions.MaxTime;

  static DateTime? GetLastMessageDate (IEnumerable<Message> messages) => messages.LastOrDefault()?.MessageDate;
}