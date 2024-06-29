
namespace Simple.App;

public sealed record ResumeMessagesJob: Job
{
  public override string JobName { get; init; } = "ResumeMessagesJob";
  public ResumeMessagesOptions ResumeMessagesOptions { get; init; } = new ();
}