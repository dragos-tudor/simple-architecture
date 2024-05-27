
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  internal static string GetMessageType<TPayload>() => typeof(TPayload).Name;
}