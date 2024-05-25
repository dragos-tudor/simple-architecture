
namespace Simple.Infrastructure.Mediator;

partial class MediatorFuncs
{
  internal static string GetTypeName<T>() => typeof(T).Name;
}