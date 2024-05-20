
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  public static Mediator CreateMediator () => new ();

  public static ThreadSafeMediator CreateThreadSafeMediator () => new ();
}