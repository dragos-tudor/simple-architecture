
namespace Simple.Domain.Integrations;

partial class IntegrationsFuncs
{
  public const string ResumeMessages = "ResumeMessages";

  static bool IsResumeMessagesJob (Job job) => job.JobName == ResumeMessages;
}