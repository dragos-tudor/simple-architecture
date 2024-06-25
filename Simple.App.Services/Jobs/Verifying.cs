
namespace Simple.App.Services;

partial class ServicesFuncs
{
  public const string ResumeMessages = "ResumeMessages";

  static bool IsResumeMessagesJob (Job job) => job.JobName == ResumeMessages;
}