
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  static string GetMessageType<TPayload>() => typeof(TPayload).Name;
}